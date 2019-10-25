using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWall : MonoBehaviour
{
    public Transform player, cam;

    GameObject last = null;


    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(player.position, cam.position - player.position, out hit, Vector3.Distance(player.position, cam.position) ))
        {
            Debug.DrawRay(player.position, cam.position - player.position, Color.red);

            if(hit.transform.gameObject != last)
            {
                last?.GetComponent<Renderer>().material.SetInt("_StencilMask", 2);
                hit.transform.gameObject.GetComponent<Renderer>().material.SetInt("_StencilMask", 1);
                last = hit.transform.gameObject;
            }
        }
        else
        {
            Debug.DrawLine(player.position, cam.position, Color.blue);
            last?.GetComponent<Renderer>().material.SetInt("_StencilMask", 2);
            last = null;
        }
    }
}
