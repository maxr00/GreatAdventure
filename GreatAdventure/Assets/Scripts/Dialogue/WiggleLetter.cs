using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleLetter : MonoBehaviour
{
    public float wiggle_height = 0.5f;
    public float wiggle_speed = 10f;
    private Vector3 prev_position;
    float t = 0.0f;
    public bool isWiggling = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isWiggling)
        {
            t += wiggle_speed * Time.deltaTime;

            // why the fuck does this not compile ??
            //GetComponent<RectTransform>().position.y += (wiggle_height * Mathf.Sin(t));

            Vector3 currentPos = GetComponent<RectTransform>().position;
            currentPos.y += (wiggle_height * Mathf.Sin(t));
            GetComponent<RectTransform>().position = currentPos;
        }
    }

    public void StartWiggle(Vector3 startPos)
    {
        isWiggling = true;
        t = 0;
    }
}
