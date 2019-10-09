using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKWalkerTarget : MonoBehaviour
{
    public float dragSpeed = 3;
    public float raiseSpeed = 5;

    public float vertAmp = 2;
    public float horizAmp = 1;

    public Vector3 offset;
    public float timeOffset = 0;

    public float verticalRamp = 0;

    [Range(0,1)] public float contactRange = 0.1f;
    public Phase phase = Phase.Drag;
    public enum Phase { ContactFront, Drag, ContactBack, Raise };

    float time = 0;

    void Update()
    {

        float phaseSpeed = phase == Phase.Raise ? raiseSpeed : dragSpeed;
        time += -Time.deltaTime * phaseSpeed;

        float x = Mathf.Cos(-(time + timeOffset));

        float percent = (x + 1) / 2.0f;
        if (percent > 1 - contactRange)
            phase = Phase.ContactFront;
        else if (percent < contactRange)
            phase = Phase.ContactBack;
        else if (Mathf.Sin(time + timeOffset) * phaseSpeed > 0) // increasing
            phase = Phase.Raise;
        else
            phase = Phase.Drag;

        Vector3 right   = Vector3.right * 0;
        Vector3 forward = Vector3.forward * horizAmp * x;
        Vector3 up      = Vector3.up * vertAmp * Mathf.Sin(time + timeOffset);

        if(verticalRamp != 0)
            up = up * verticalRamp * percent;

        transform.localPosition = offset + right + up + forward;
    }
}
