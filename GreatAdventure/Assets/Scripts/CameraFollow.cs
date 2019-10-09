using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 distance;

    public CinemachineTargetGroup group;
    bool grouped = false;
    public float groupRadius;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        transform.position = offset + target.position;

        if(DialogueComponent.currentActiveDialogue != null)
        {
            if(!grouped)
            {
                grouped = true;

                var characters = DialogueComponent.currentActiveDialogue.m_dialogueAsset.m_characterData.Values;

                group.m_Targets = new CinemachineTargetGroup.Target[0]; // reset members

                foreach (var c in characters)
                {
                    group.AddMember(c.transform, 1.0f / characters.Count, c.cameraRadius);
                }
            }
        }
        else
        {
            if(grouped)
            {
                grouped = false;
            }
        }
    }
}
