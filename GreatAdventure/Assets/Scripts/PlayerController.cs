using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public float maxSpeed = 10;
    public float burstSpeed = 20;

    public float turnThreshold = 0.8f;
    public float turnSlowdown = 1.1f;
    public float turnSpeedup = 4.0f;

    public float groundCheckHeight = 1.2f;
    public float bodyRadius = 0.5f;
    public float stoppedSlowing = 1.2f;

    public float standingGravity = 9.8f / 2;
    public float fallGravity = 9.8f * 2;

    public LayerMask groundLayer;

    [HideInInspector]
    public bool interacting;

    Rigidbody rbody;
    CharacterAnimationController animController;

    bool wasMoving = true;

    public bool allowMovement = true;

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
        animController = GetComponent<CharacterAnimationController>();
    }

    void FixedUpdate()
    {

        float horiz = Input.GetAxisRaw("Horizontal");
        float vert  = Input.GetAxisRaw("Vertical");

        var forward = new Vector3(horiz, 0, vert).normalized;
        if (!allowMovement)
            forward = Vector3.zero;

        // Angle check
        Debug.DrawLine(transform.position, transform.position - transform.up * groundCheckHeight, Color.blue);
        Debug.DrawLine(transform.position + forward, transform.position + forward - transform.up * groundCheckHeight, Color.gray);


        if( Physics.Raycast(new Ray(transform.position, -Vector3.up), out var hit, groundCheckHeight, groundLayer) // Directly down
            || Physics.Raycast(new Ray(transform.position + forward * bodyRadius, -Vector3.up), out var hitEdge, groundCheckHeight, groundLayer)) // Stuck on edge check
        {
            // Grounded
            rbody.AddForce(Vector3.down * standingGravity, ForceMode.Acceleration); // Stay on ground

            // Ramp check
            Physics.Raycast(new Ray(transform.position + forward * bodyRadius, -Vector3.up), out var hitRamp, groundCheckHeight, groundLayer);

            var move = horiz * Vector3.right + vert * Vector3.forward;
            //move = Vector3.ProjectOnPlane(move, hitRamp.normal).normalized;

            Debug.DrawLine(transform.position, transform.position + move, Color.red);

            if(wasMoving)
            {
                if (Vector3.Dot(rbody.velocity.normalized, move) < turnThreshold)
                {
                    // If turning around, make it faster but slowing fighting velocity 
                    rbody.velocity = new Vector3(rbody.velocity.x / turnSlowdown, rbody.velocity.y, rbody.velocity.z / turnSlowdown);
                    rbody.AddForce(move * moveSpeed * turnSpeedup, ForceMode.Impulse);
                }
                else
                {
                    // Continue movement force
                    rbody.AddForce(move * moveSpeed, ForceMode.Impulse);
                }

                //if((transform.forward + Vector3.ProjectOnPlane(rbody.velocity, hitRamp.normal).normalized).sqrMagnitude > 0.1f)
                //    transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(rbody.velocity, hitRamp.normal).normalized, 0.3f); 
                //else
                //    transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(rbody.velocity, hitRamp.normal).normalized, 0.6f); // Turn around 180 degrees

                Vector3 dir = Vector3.forward;
                float min = 0;
                foreach(Vector3 d in forwardDirs)
                {
                    if(min < Vector3.Dot(d, rbody.velocity.normalized))
                    {
                        dir = d;
                        min = Vector3.Dot(d, rbody.velocity.normalized);
                    }
                }

                transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(dir, hitRamp.normal), 0.6f);
            }
            else
            {
                rbody.AddForce(move * burstSpeed, ForceMode.Impulse);
            }
        }
        else
        {
            // Not on ground
            rbody.AddForce(Vector3.down * fallGravity, ForceMode.Acceleration); // Fall faster
        }

        if (forward.sqrMagnitude < 0.025f) // if stopped input, slow down to stop
        {
            rbody.velocity = new Vector3(rbody.velocity.x / stoppedSlowing, rbody.velocity.y, rbody.velocity.z / stoppedSlowing);
        }

        if(rbody.velocity.sqrMagnitude < 0.1f) // has the player stopped moving / is standing still?
        {
            wasMoving = false;
        }
        else
        {
            wasMoving = true;
        }

        float vel = rbody.velocity.magnitude;
        if (vel >= maxSpeed)
        {
            rbody.velocity = new Vector3(rbody.velocity.x * maxSpeed / vel, rbody.velocity.y * maxSpeed / vel, rbody.velocity.z * maxSpeed / vel);
        }

        // Update animation controller details
        animController.movement = forward;
    }

    private void Update()
    {
        interacting = Input.GetAxisRaw("Interact") != 0;
    }
}
