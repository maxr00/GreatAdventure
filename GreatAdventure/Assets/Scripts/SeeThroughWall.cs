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
                    string isMaskable = hitRend.material.GetTag("Maskable", true, "false");
                    if (isMaskable != "false")
                    {
                        hitRend.material.SetInt("_StencilMask", 1);
                        hitRend.material.renderQueue = 3000; // transparent
                    }
                }
            }
        }
        pastHits = hits;
    }
}
