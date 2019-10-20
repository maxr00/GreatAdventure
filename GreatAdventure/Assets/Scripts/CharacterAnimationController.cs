using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator anim;
    Rigidbody rbody;

    public Vector3 movement; // Set by player controller

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horiz, 0, vert).normalized;

        if (movement.sqrMagnitude > 0.1f && rbody.velocity.sqrMagnitude > 0.1f)
        {
            anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }
    }
}
