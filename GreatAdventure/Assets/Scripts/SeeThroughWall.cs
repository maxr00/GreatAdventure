using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWall : MonoBehaviour
{
    public Transform player, cam;

    //GameObject last = null;
    RaycastHit[] pastHits;

    void Start()
    {
        
    }

    void Update()
    {
        if (pastHits != null)
        {
            foreach (var hit in pastHits)
            {
                Debug.DrawLine(player.position, cam.position, Color.blue);
                Renderer hitRend = hit.transform.gameObject.GetComponent<Renderer>();

                if (hitRend != null)
                {
                    Debug.DrawLine(player.position, cam.position, Color.blue);
                    hitRend.material.renderQueue = -1; // from shader
                    hitRend.material.SetInt("_StencilMask", 2);
                }
            }
        }

        RaycastHit[] hits;
        hits = Physics.RaycastAll(player.position, cam.position - player.position, Vector3.Distance(player.position, cam.position));
        if (hits.Length > 0)
        {
            Debug.DrawRay(player.position, cam.position - player.position, Color.red);

            foreach (var hit in hits)
            {
                Renderer hitRend = hit.transform.gameObject.GetComponent<Renderer>();

                if (hitRend != null)
                {
                    //last?.GetComponent<Renderer>()?.material.SetInt("_StencilMask", 2);
                    hitRend.material.SetInt("_StencilMask", 1);
                    //last = hit.transform.gameObject;
                    hitRend.material.renderQueue = 3000; // transparent
                }
            }
        }
        pastHits = hits;



        //RaycastHit hit;
        
        //if (Physics.Raycast(player.position, cam.position - player.position, out hit, Vector3.Distance(player.position, cam.position) ))
        //{
        //    if (hit.transform.gameObject.GetComponent<Renderer>() == null) { return; }
        //    Debug.DrawRay(player.position, cam.position - player.position, Color.red);

        //    if(hit.transform.gameObject != last)
        //    {
        //        last?.GetComponent<Renderer>()?.material.SetInt("_StencilMask", 2);
        //        hit.transform.gameObject.GetComponent<Renderer>()?.material.SetInt("_StencilMask", 1);
        //        last = hit.transform.gameObject;
        //        last.GetComponent<Renderer>().material.renderQueue = 3000; // transparent
        //    }
        //}
        //else
        //{
        //    Debug.DrawLine(player.position, cam.position, Color.blue);
        //    if (last != null) last.GetComponent<Renderer>().material.renderQueue = -1; // from shader
        //    last?.GetComponent<Renderer>()?.material.SetInt("_StencilMask", 2);
        //    last = null;
        //}
    }
}
