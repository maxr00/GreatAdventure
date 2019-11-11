using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public Transform carBody;
	Rigidbody rbody;
	WheelTurner wheelTurner;

	Vector3 wheelsForward;

	public float speed = 40;
	public float reverseSpeed = 20;
	public float turnSpeed = 50;
	public float wheelTurnSpeed = 4;
	public float wheelCorrectionSpeed = 2;
	public float correctionSpeed = 2;
    public float turnTilt = 20;
    public float tiltSpeed = 2;

    public float groundCheckHeight = 0.2f;

	public float frontWheelDriveAmt = 0.2f;
	public float backWheelDriveAmt = 1.0f;

	public Vector2 groundDrag = new Vector2(1.5f, 1);
	public Vector2 airDrag = new Vector2(1.5f, 1);
    public float fallForce = 18;
    bool inAir = false;

    [Header("Drift")]

    public bool drifting = false;
    public bool stoppedDrifting = false;
    public float driftTurn = 0.3f;
    public float driftTurnTorque = 1.5f;
    public float driftAgainstTurnTorque = 0.2f;
    public float stoppedDriftingTurnSpeedBoost = 5;
    public float stoppedDriftingTurnTorqueBoost = 10;
    public float stoppedDriftingBuffTime = 0.3f;
    public float driftSpeedMod = 0.7f;
    public float driftHopForce = 20;
    public ParticleSystem driftParticles;
    public float driftAutostop = 0.3f;
    public float driftBoostTime = 1.25f;
    public float driftTilt = 30;
    public float driftTiltSpeed = 4;

    [Header("Boost")]

    public float boostBonusSpeed = 40;
    public float boostTime = 0.5f;
    public bool boosting = false;
    public ParticleSystem boostParticles;

    private float driftTime = 0;
    private float driftDirection = 0;
    private float stoppedDriftingTimer = 0;
    private float autostopDriftingTimer = 0;
    private float boostTimer = 0;

    private float currBoost = 0;

    [Header("Slam")]
    public Transform slamTarget;
    public float slamDistance = 3;
    public float slamForce = 100;
    public float slamUpForce = 5;
    public float slamCooldown = 2;
    public float slamWiggle = 3;
    public int slamRocks = 2;
    public float slamRockAmplitude = 2;
    public float slamRecoveryMaxSpeed = 0.5f;
    public bool recoveringSlam = false;

    private float slamTimer = 0;

    private float tiltH = 0;

    private void Update()
	{
		if (Vector3.Dot(transform.up, Vector3.up) <= 0 && !inAir)
		{
			transform.up = Vector3.up;
			transform.position += Vector3.up;
		}
	}

	// Use this for initialization
	void Start ()
	{
		rbody = GetComponent<Rigidbody>();
		wheelTurner = GetComponent<WheelTurner>();
		wheelsForward = transform.forward;
	}
    
	// Update is called once per frame
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

        HandleDrifting(h, v);

        HandleBoost();

        HandleSlam(ref h, ref v);

        if (Input.GetAxisRaw("Interact") != 0)
            Slam(h,v);
        
        // Turning
        if (h != 0)
		{
            if(!drifting)
            {
                if(!stoppedDrifting)
                {
			        wheelsForward = Vector3.Lerp(wheelsForward, transform.right * h, Time.deltaTime * wheelTurnSpeed);
			        wheelsForward = Vector3.ProjectOnPlane(wheelsForward, transform.up);
                }
                else
                {
                    wheelsForward = Vector3.Lerp(wheelsForward, transform.right * h, Time.deltaTime * (wheelTurnSpeed + stoppedDriftingTurnSpeedBoost) );
                    wheelsForward = Vector3.ProjectOnPlane(wheelsForward, transform.up);
                }
            }
            else
            {
                wheelsForward = Vector3.Lerp(wheelsForward, transform.right * (h + driftTurn * -Mathf.Sign(h)), Time.deltaTime * wheelTurnSpeed);
                wheelsForward = Vector3.ProjectOnPlane(wheelsForward, transform.up);
            }

        }

        // Tilt
        tiltH = Mathf.Lerp(tiltH, h, Time.deltaTime * (drifting ? driftTiltSpeed : tiltSpeed));
        float tiltAmt = drifting ? driftTilt : turnTilt;
        carBody.localRotation = Quaternion.Euler(0, 0, Mathf.Abs(tiltH) * Vector3.Dot(wheelsForward, transform.right) * tiltAmt);

        //if (Mathf.Sign(tiltH) == Mathf.Sign(Vector3.Dot(wheelsForward, transform.right)))
        //    carBody.localRotation = q;
        //else
        //{
        //    carBody.localRotation = Quaternion.Lerp(carBody.localRotation, Quaternion.identity, Time.deltaTime);
        //}



        if (inAir) // was in air last update
		{
			rbody.drag = groundDrag.x;
			rbody.angularDrag = groundDrag.y;
			inAir = false;
		}

        // Ground check
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckHeight, Color.red);
        RaycastHit hit;
        if (Physics.Linecast(transform.position, transform.position - Vector3.up * groundCheckHeight, out hit))
		{
            rbody.drag = groundDrag.x + hit.collider.material.dynamicFriction;
            rbody.angularDrag = groundDrag.y + hit.collider.material.staticFriction;
        }
        else
        {
			rbody.drag = airDrag.x;
			rbody.angularDrag = airDrag.y;
			inAir = true;

            rbody.AddForce(Vector3.down * fallForce, ForceMode.Acceleration);

			wheelTurner.Turn(wheelsForward);
        }
        
        // Acceleration
		if (v != 0)
		{
			float s = v > 0 ? speed + currBoost : reverseSpeed;
			
            if(!drifting)
            {
		        rbody.AddForce((wheelsForward * frontWheelDriveAmt + transform.forward * backWheelDriveAmt).normalized * v * s, ForceMode.Acceleration);

                // Turning Torque
		        if (h != 0)
		        {
                    float bonus = stoppedDrifting ? stoppedDriftingTurnTorqueBoost : 0;
				    rbody.AddTorque(transform.up * h * (turnSpeed + bonus) * Mathf.Sign(v), ForceMode.Acceleration);
		        }
            }
            else
            {
                rbody.AddForce((wheelsForward * frontWheelDriveAmt + transform.forward * backWheelDriveAmt).normalized * v * s * driftSpeedMod, ForceMode.Acceleration);

                // Drift Turning Torque
                if(Mathf.Sign(h) == driftDirection)
                    rbody.AddTorque(transform.up * (h * driftTurnTorque) * turnSpeed * Mathf.Sign(v), ForceMode.Acceleration);
                else
                    rbody.AddTorque(transform.up * (h * driftAgainstTurnTorque) * turnSpeed * Mathf.Sign(v), ForceMode.Acceleration);
            }


			wheelsForward = Vector3.Lerp(wheelsForward, transform.forward, Time.deltaTime * wheelCorrectionSpeed);
			wheelsForward = Vector3.ProjectOnPlane(wheelsForward, Vector3.up);

            float dot = Vector3.Dot(wheelsForward, transform.right);
			rbody.AddTorque(transform.up * (1 - dot) * (correctionSpeed * Mathf.Sign(dot)), ForceMode.Acceleration);
		}
		
		Debug.DrawLine(transform.position, transform.position + wheelsForward * 3, Color.blue);

		if(v >= 0)
			wheelTurner.Turn(wheelsForward);
		else
			wheelTurner.Turn(Vector3.Cross(wheelsForward, transform.up)); // Reverse

        // Jump
        //if (Input.GetKeyDown(KeyCode.Space))
        //	rbody.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
    }

    void HandleDrifting(float h, float v)
    {
        // Drifting controls
        var drift = Input.GetKey(KeyCode.LeftShift);

        // Autostopper
        if(!drift)
        {
            autostopDriftingTimer = 0;
        }

        if (autostopDriftingTimer >= driftAutostop)
        {
            drift = false; // stop drifting
        }
        else if (drifting)
        {
            driftTime += Time.deltaTime;

            if (Mathf.Sign(h) != driftDirection || h == 0 || v == 0)
                autostopDriftingTimer += Time.deltaTime;
            else
                autostopDriftingTimer = 0;
        }

        // First update of drift
        if (drift && !drifting && !inAir)
        {
            // Hop
            rbody.AddForce(Vector3.up * driftHopForce, ForceMode.Impulse);

            driftDirection = Mathf.Sign(h);
        }

        // Stopped drift buff
        if (!drift && drifting)
        {
            // Just stopped drifting
            stoppedDrifting = true;

            if(driftTime >= driftBoostTime)
            {
                Boost();
            }
            driftTime = 0;
        }

        if (stoppedDrifting)
        {
            stoppedDriftingTimer += Time.deltaTime;
            if (stoppedDriftingTimer > stoppedDriftingBuffTime)
            {
                stoppedDrifting = false;
                stoppedDriftingTimer = 0;
            }
        }

        // Particles
        var emission = driftParticles.emission;
        emission.enabled = (drifting && !inAir);

        drifting = drift;
    }

    public void Boost()
    {
        boosting = true;
    }

    void HandleBoost()
    {
        if(boosting)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer >= boostTime)
            {
                boosting = false;
                boostTimer = 0;
                currBoost = 0;
            }
            else
            {
                currBoost = boostBonusSpeed;
            }
        }

        // Particles
        var emission = boostParticles.emission;
        emission.enabled = boosting;
    }

    void Slam(float h, float v)
    {
        if (recoveringSlam)
            return;

        if (Vector3.Distance(transform.position, slamTarget.position) > slamDistance)
            return;

        Vector3 dir = (slamTarget.position - transform.position).normalized;
        dir.y = 0;

        //Vector3 dir = transform.forward * v;
        //if (Mathf.Abs(h) > 0.5)
        //    dir = transform.right * h;

        rbody.AddForce(dir * slamForce + Vector3.up * slamUpForce, ForceMode.Impulse);
        slamTimer = slamCooldown;
        recoveringSlam = true;
    }

    void HandleSlam(ref float h, ref float v)
    {
        if (!recoveringSlam)
            return;

        slamTimer -= Time.deltaTime;

        if(slamTimer <= 0)
        {
            slamTimer = 0;
            recoveringSlam = false;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            carBody.localRotation = Quaternion.identity;
            return;
        }

        float wiggle = Mathf.Sin((slamTimer / slamCooldown) * Mathf.PI * slamWiggle + Mathf.PI);

        rbody.AddTorque(transform.up * wiggle * 100, ForceMode.Acceleration);

        float percent = 1 - (slamTimer) / slamCooldown;
        float rock = slamRockAmplitude * Mathf.Sin(percent * slamRocks * (2 * Mathf.PI));

        carBody.localRotation = Quaternion.Euler(0, 0, rock);
        //rbody.AddTorque(transform.forward * rock, ForceMode.VelocityChange);


        h = Mathf.Clamp(h * 0.25f + wiggle, -1, 1);
        v = Mathf.Clamp(v, -slamRecoveryMaxSpeed, slamRecoveryMaxSpeed);
    }
}
