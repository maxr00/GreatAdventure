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
    public bool isScreenSpace = true;

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
            if (!isScreenSpace)
            {
                Vector3 currentPos = GetComponent<RectTransform>().position;
                currentPos.y += (wiggle_height * Mathf.Sin(t));
                GetComponent<RectTransform>().position = currentPos;
            }
            else
            {
                GetComponent<CharacterText>().top += (wiggle_height * Mathf.Sin(t));
            }
        }
    }

    public void StartWiggle(bool ScreenSpace)
    {
        isWiggling = true; isScreenSpace = ScreenSpace;
        t = 0;
    }
}
