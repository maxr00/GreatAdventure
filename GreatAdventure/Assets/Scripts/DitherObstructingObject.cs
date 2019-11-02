﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitherObstructingObject : MonoBehaviour
{
    public Transform player, cam;

    //GameObject last = null;


    Shader normalShader;
    Shader ditherShader;

    RaycastHit[] pastHits;
    // Start is called before the first frame update
    void Start()
    {
        normalShader = Shader.Find("Custom/CartoonShader");
        ditherShader = Shader.Find("Custom/CartoonShaderDitherTransparency");
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
                hit.transform.gameObject.GetComponent<Renderer>().material.shader = normalShader;
            }
        }

        RaycastHit[] hits;
        hits = Physics.RaycastAll(player.position, cam.position - player.position, Vector3.Distance(player.position, cam.position));
        if (hits.Length > 0)
        {
            Debug.DrawRay(player.position, cam.position - player.position, Color.red);

            foreach(var hit in hits)
            {
                GameObject hitObject = hit.transform.gameObject;
                hitObject.GetComponent<Renderer>().material.shader = ditherShader;
                hitObject.GetComponent<Renderer>().material.renderQueue = 3000;
                hitObject.GetComponent<Renderer>().material.SetFloat("_Transparency", 0.5f);
            }

            //if (hit.transform.gameObject != last)
            //{
            //    last = hit.transform.gameObject;
            //    last.GetComponent<Renderer>().material.shader = ditherShader;
            //    last.GetComponent<Renderer>().material.renderQueue = 3000;
            //    hit.transform.gameObject.GetComponent<Renderer>().material.SetFloat("_Transparency", 0.5f);
            //}
        }
        pastHits = hits;
        //else
        //{
        //    Debug.DrawLine(player.position, cam.position, Color.blue);
        //    if (last != null)
        //        last.GetComponent<Renderer>().material.shader = normalShader;
        //    last = null;
        //}
    }
}
