﻿using System.Collections;
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
    public float jumpGravity = 9.8f * 3;

    public float jumpForce = 10;
    public float standingJumpForce = 7.5f;
    bool jumpPressed = false;
    bool jumping = false;
    Vector3 jumpMove;

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


        bool grounded = Physics.Raycast(new Ray(transform.position, -Vector3.up), out var hit, groundCheckHeight, groundLayer) // Directly down
                     || Physics.Raycast(new Ray(transform.position + forward * bodyRadius, -Vector3.up), out var hitEdge, groundCheckHeight, groundLayer); // Stuck on edge check

        if (grounded && jumping && rbody.velocity.y <= 0)
        {
            jumping = false; // Grounded and not ascending, not jumping
            wasMoving = false; // Give boost on land
        }

        if (grounded && !jumping)
        {
            // Grounded
            rbody.AddForce(Vector3.down * standingGravity, ForceMode.Acceleration); // Stay on ground

            // Ramp check
            Physics.Raycast(new Ray(transform.position + forward * bodyRadius, -Vector3.up), out var hitRamp, groundCheckHeight, groundLayer);

            var move = horiz * Vector3.right + vert * Vector3.forward; // aka unnormalized forward
            if (!allowMovement)
                move = Vector3.zero;
            //move = move.normalized; // feels better without
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

                // Smooth Turning:
                /*
                    if((transform.forward + Vector3.ProjectOnPlane(rbody.velocity, hitRamp.normal).normalized).sqrMagnitude > 0.1f)
                        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(rbody.velocity, hitRamp.normal).normalized, 0.3f); 
                    else
                        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(rbody.velocity, hitRamp.normal).normalized, 0.6f); // Turn around 180 degrees
                */

                // 8-Dir Turning:
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
                //  Just started moving burst
                rbody.AddForce(move * burstSpeed, ForceMode.Impulse);
            }

            // Jump
            if (allowMovement && Input.GetAxisRaw("Jump") != 0 && !jumpPressed && !jumping)
            {
                if(move == Vector3.zero)
                    rbody.AddForce(Vector3.up * standingJumpForce, ForceMode.Impulse);
                else
                    rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumping = true;
                jumpMove = move;
            }
        }
        else if(jumping)
        {
            rbody.AddForce(jumpMove * moveSpeed, ForceMode.Impulse);
            rbody.AddForce(Vector3.down * jumpGravity, ForceMode.Acceleration); // Fall faster
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

        if (Input.GetAxisRaw("Jump") != 0)
            jumpPressed = true;
        else
            jumpPressed = false;

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
        if (allowMovement)
            interacting = Input.GetAxisRaw("Interact") != 0;
        else
            interacting = false;
    }
}
