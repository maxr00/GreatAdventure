using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefMovement : MonoBehaviour
{
    public Transform carBody;
    public Transform player;

    public Transform[] waypoints;
    public float waypointStep = 5;
    public int progressionDir = 1;
    public float repickDistance = 2;

    private int target;
    private List<Vector3> steppedWaypoints = new List<Vector3>();

    Rigidbody rbody;
    WheelTurner wheelTurner;

    Vector3 wheelsForward;

    public float speed = 40;
    public float reverseSpeed = 20;
    public float turnSpeed = 50;
    public float turnSpeedBoost = 5;
    public float wheelTurnSpeed = 4;
    public float wheelCorrectionSpeed = 2;
    public float correctionSpeed = 2;
    public float turnTilt = 20;
    public float tiltSpeed = 2;
    public float normalRotationSpeed = 10;

    public float groundCheckHeight = 0.2f;

    public float frontWheelDriveAmt = 0.2f;
    public float backWheelDriveAmt = 1.0f;

    public Vector2 groundDrag = new Vector2(1.5f, 1);
    public Vector2 airDrag = new Vector2(1.5f, 1);
    public float fallForce = 18;
    bool inAir = false;

    public bool allowMovement = true;

    private float tiltH = 0;

    Vector3[] forwardDirs =
        {
            Vector3.forward,
            (Vector3.forward + Vector3.right).normalized,
            Vector3.right,
            (-Vector3.forward + Vector3.right).normalized,
            -Vector3.forward,
            (-Vector3.forward - Vector3.right).normalized,
            -Vector3.right,
            (Vector3.forward - Vector3.right).normalized,
        };

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        wheelTurner = GetComponent<WheelTurner>();
        wheelsForward = transform.forward;

        //steppedWaypoints = new Vector3[waypoints.Length * waypointSteps];
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 curr = waypoints[i].position;
            Vector3 next = waypoints[(i + 1) % waypoints.Length].position;

            int numSteps = 0; // just in case something bad happens, dont eat all of my memory in an infinite loop
            for(Vector3 w = curr; w != next && numSteps < 100;)
            {
                numSteps++;
                steppedWaypoints.Add(w);

                if (Vector3.Distance(w, next) > waypointStep + (waypointStep / 5.0f))
                    w += (next - curr).normalized * waypointStep;
                else
                    break;
            }
        }

        for (int i = 0; i < steppedWaypoints.Count; i++)
        {
            Vector3 prev = steppedWaypoints[(i - 1 + steppedWaypoints.Count) % steppedWaypoints.Count];
            Vector3 curr = steppedWaypoints[i];
            Vector3 next = steppedWaypoints[(i + 1) % steppedWaypoints.Count];

            Vector3 a = Vector3.Lerp(prev, curr, 0.5f);

            steppedWaypoints[i] = Vector3.Lerp(a, next, 0.5f);
        }

    }

    void FixedUpdate()
    {
        float dist = Vector3.Distance(
            new Vector3(steppedWaypoints[this.target].x, 0, steppedWaypoints[this.target].z), 
            new Vector3(transform.position.x, 0, transform.position.z));
        if (dist < repickDistance)
        {
            this.target = PickNextTarget();
        }

        Vector3 target = steppedWaypoints[this.target];

        // Debug draw
        foreach (Vector3 a in steppedWaypoints)
        {
            if(a == target)
                Debug.DrawLine(a, a + Vector3.up, Color.red);
            else
                Debug.DrawLine(a, a + Vector3.up, Color.white);
        }
        Debug.DrawLine(transform.position, target, Color.blue);

        Vector3 dir = (target - transform.position).normalized;

        float h = Vector3.Dot(dir, transform.right);

        float v = Mathf.Max(
            Mathf.Abs(Vector3.Dot(dir, transform.forward)),
            Mathf.Abs(Vector3.Dot(dir, transform.right) / 2));

        if (allowMovement)
            CarFixedUpdate(h, v);
    }

    void CarFixedUpdate(float h, float v)
    {
        //HandleDrifting(h, v);
        //HandleBoost();
        //HandleSlam(ref h, ref v);
        //if (Input.GetAxisRaw("Interact") != 0)
        //    Slam(h, v);

        // Turning
        if (h != 0)
        {
            wheelsForward = Vector3.Lerp(wheelsForward, transform.right * h, Time.deltaTime * wheelTurnSpeed);
            wheelsForward = Vector3.ProjectOnPlane(wheelsForward, transform.up);
        }

        // Tilt

        RaycastHit groundHit, rampHit;
        if (Physics.Raycast(transform.position, -Vector3.up, out groundHit))
        {
            tiltH = Mathf.Lerp(tiltH, h, Time.deltaTime * tiltSpeed);
            float tiltAmt = turnTilt;
            
            int sign = 1;
            if (Physics.Raycast(transform.position + transform.forward * 0.1f, -Vector3.up, out rampHit) && rampHit.point.y > groundHit.point.y)
                sign = -1;

            carBody.localRotation = Quaternion.Euler(0, 0, Mathf.Abs(tiltH) * Vector3.Dot(wheelsForward, transform.right) * tiltAmt);

            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(sign * Vector3.Angle(Vector3.up, groundHit.normal), transform.localRotation.eulerAngles.y, 0),
                Time.deltaTime * normalRotationSpeed);
        }

        //tiltH = Mathf.Lerp(tiltH, h, Time.deltaTime * tiltSpeed);
        //float tiltAmt = turnTilt;
        //carBody.localRotation = Quaternion.Euler(0, 0, Mathf.Abs(tiltH) * Vector3.Dot(wheelsForward, transform.right) * tiltAmt);

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
            float s = v > 0 ? speed + (Mathf.Abs(h) * turnSpeedBoost) : reverseSpeed;

            rbody.AddForce((wheelsForward * frontWheelDriveAmt + transform.forward * backWheelDriveAmt).normalized * v * s, ForceMode.Acceleration);

            // Turning Torque
            if (h != 0)
            {
                rbody.AddTorque(transform.up * h * turnSpeed * Mathf.Sign(v), ForceMode.Acceleration);
            }

            wheelsForward = Vector3.Lerp(wheelsForward, transform.forward, Time.deltaTime * wheelCorrectionSpeed);
            wheelsForward = Vector3.ProjectOnPlane(wheelsForward, Vector3.up);

            float dot = Vector3.Dot(wheelsForward, transform.right);
            rbody.AddTorque(transform.up * (1 - dot) * (correctionSpeed * Mathf.Sign(dot)), ForceMode.Acceleration);
        }

        Debug.DrawLine(transform.position, transform.position + wheelsForward * 3, Color.blue);

        if (v >= 0)
            wheelTurner.Turn(wheelsForward);
        else
            wheelTurner.Turn(Vector3.Cross(wheelsForward, transform.up)); // Reverse
    }

    int PickNextTarget()
    {
        int curr = this.target;
        int next = (curr + progressionDir + steppedWaypoints.Count) % steppedWaypoints.Count;
        int prev = (curr - progressionDir + steppedWaypoints.Count) % steppedWaypoints.Count;

        return next;

        /*
        float nextDist = Vector3.Distance(steppedWaypoints[next], player.position);
        float prevDist = Vector3.Distance(steppedWaypoints[prev], player.position);

        if(Vector3.Distance(player.position, transform.position) < Vector3.Distance(transform.position, steppedWaypoints[next]))
        {
            // Player is closer than next target, may be approaching from front
            //Debug.Log(Vector3.Distance(player.position, transform.position) + " < " + nextDist);

            if (nextDist > prevDist)
                return next; // Next in list is further from player
            progressionDir *= -1;
            return prev; // Previous in list is further from player
        }
        else
            return next;
        */
    }
}
