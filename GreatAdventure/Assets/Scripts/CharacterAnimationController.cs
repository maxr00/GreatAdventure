using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator anim;
    Rigidbody rbody;

    public bool useMovement = true;
    public Vector3 movement; // Set by player/character controller

    public float runThreshold;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horiz, 0, vert).normalized;

        if ((!useMovement || movement.sqrMagnitude > 0.1f) && rbody.velocity.sqrMagnitude > 0.1f)
        {
            if (rbody.velocity.magnitude >= runThreshold)
                anim.Play("Run");
            else
                anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }
    }
}
