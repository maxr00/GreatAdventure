using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK2D_Controller : MonoBehaviour
{
    /*
     Code derived from Alan Zucconi
     https://www.alanzucconi.com/2018/05/02/ik-2d-2/
    */

    public Transform Joint0;
    public Transform Joint1;
    public Transform Hand;

    public Transform Target;

    private float length0;
    private float length1;
 
    void Start()
    {
        length0 = Vector3.Distance(Joint0.position, Joint1.position);
        length1 = Vector3.Distance(Joint1.position, Hand.position);
    }

    void Update()
    {
        float jointAngle0;
        float jointAngle1;

        float length2 = Vector2.Distance(Joint0.position, Target.position);

        // Angle from Joint0 and Target
        Vector3 diff = Target.position - Joint0.position;
        float atan = Mathf.Atan2(diff.y, diff.z) * Mathf.Rad2Deg;

        // Is the target reachable?
        // If not, we stretch as far as possible
        if (length0 + length1 < length2)
        {
            jointAngle0 = atan;
            jointAngle1 = 0f;
        }
        else
        {
            float cosAngle0 = ((length2 * length2) + (length0 * length0) - (length1 * length1)) / (2 * length2 * length0);
            float angle0 = Mathf.Acos(cosAngle0) * Mathf.Rad2Deg;

            float cosAngle1 = ((length1 * length1) + (length0 * length0) - (length2 * length2)) / (2 * length1 * length0);
            float angle1 = Mathf.Acos(cosAngle1) * Mathf.Rad2Deg;

            // So they work in Unity reference frame
            jointAngle0 = atan - angle0;
            jointAngle1 = 180f - angle1;
        }
        
        Vector3 Euler0 = Joint0.transform.localEulerAngles;
        Euler0.x = jointAngle0;
        Joint0.transform.localEulerAngles = Euler0;

        Vector3 Euler1 = Joint1.transform.localEulerAngles;
        Euler1.x = jointAngle1;
        Joint1.transform.localEulerAngles = Euler1;
    }
}
