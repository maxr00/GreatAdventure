using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefMovement : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;
    public float waypointStep = 5;

    public List<Vector3> steppedWaypoints = new List<Vector3>();
    public int target;
    public int progressionDir = 1;

    public float repickDistance = 2;
    
    public float moveSpeed = 1.2f;
    public float maxSpeed = 15;
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

    public float navMeshDist = 10;
    public bool isNavMesh = false;
    public float navMoveSpeed = 5;

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
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<NavMeshAgent>().speed = navMoveSpeed;
        }

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

            //steppedWaypoints[i] = Vector3.Lerp(waypoints[i / 5].position, waypoints[(i / 5 + 1) % waypoints.Length].position, (i % 5) / 5.0f);
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

        //if not on screen, use navmesha agent
        dist = Vector3.Distance(target, transform.position);
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            if (dist >= navMeshDist)
            {
                isNavMesh = true;
                agent.isStopped = false;
                agent.destination = target;
                gameObject.GetComponent<NavMeshAgent>().speed = navMoveSpeed;
                return;
            }
            else
            {
                isNavMesh = false;
                agent.isStopped = true;
            }
        }

        var forward = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z).normalized;
        if (!allowMovement)
            forward = Vector3.zero;

        // Angle check
        Debug.DrawLine(transform.position, transform.position - transform.up * groundCheckHeight, Color.blue);
        Debug.DrawLine(transform.position + forward, transform.position + forward - transform.up * groundCheckHeight, Color.gray);

        if (Physics.Raycast(new Ray(transform.position, -Vector3.up), out var hit, groundCheckHeight, groundLayer) // Directly down
            || Physics.Raycast(new Ray(transform.position + forward * bodyRadius, -Vector3.up), out var hitEdge, groundCheckHeight, groundLayer)) // Stuck on edge check
        {
            // Grounded
            rbody.AddForce(Vector3.down * standingGravity, ForceMode.Acceleration); // Stay on ground

            // Ramp check
            Physics.Raycast(new Ray(transform.position + forward * bodyRadius, -Vector3.up), out var hitRamp, groundCheckHeight, groundLayer);

            var move = forward.x * Vector3.right + forward.z * Vector3.forward;
            //move = Vector3.ProjectOnPlane(move, hitRamp.normal).normalized;

            Debug.DrawLine(transform.position, transform.position + move, Color.red);

            if (wasMoving)
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
                foreach (Vector3 d in forwardDirs)
                {
                    if (min < Vector3.Dot(d, rbody.velocity.normalized))
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

        if (rbody.velocity.sqrMagnitude < 0.1f) // has the player stopped moving / is standing still?
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
        //animController.movement = Vector3.forward;
    }

    int PickNextTarget()
    {
        int curr = this.target;
        int next = (curr + progressionDir + steppedWaypoints.Count) % steppedWaypoints.Count;
        int prev = (curr - progressionDir + steppedWaypoints.Count) % steppedWaypoints.Count;

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

    }
}
