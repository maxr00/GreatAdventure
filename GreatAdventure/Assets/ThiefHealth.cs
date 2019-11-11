using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefHealth : MonoBehaviour
{
    public int health = 3;
    public float cooldown = 2;

    private float timer = 0;

    void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timer > 0)
            return;

        if(collision.gameObject.tag == "PlayerCar")
        {
            if(collision.gameObject.GetComponent<CarControls>().recoveringSlam)
            {
                health--;
                timer = cooldown;
            }
        }
    }
}
