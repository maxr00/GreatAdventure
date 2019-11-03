using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitherObstructingObject : MonoBehaviour
{
    public Transform player, cam;
    public float DitherTransparency = 0.7f;

    RaycastHit[] pastHits;
    // Start is called before the first frame update
    void Start()
    {
        pastHits = null;
    }

    // Update is called once per frame
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
                    hitRend.material.renderQueue = -1; // from shader
                    hitRend.material.SetFloat("_Transparency", 1.0f);
                }
            }
        }

        RaycastHit[] hits;
        hits = Physics.RaycastAll(player.position, cam.position - player.position, Vector3.Distance(player.position, cam.position));
        if (hits.Length > 0)
        {
            Debug.DrawRay(player.position, cam.position - player.position, Color.red);

            foreach(var hit in hits)
            {
                Renderer hitRend = hit.transform.gameObject.GetComponent<Renderer>();

                if (hitRend != null)
                {
                    hitRend.material.renderQueue = 3000; // transparent
                    hitRend.material.SetFloat("_Transparency", DitherTransparency);
                }
            }
        }
        pastHits = hits;
    }
}
