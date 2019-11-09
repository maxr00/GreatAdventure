using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeLetter : MonoBehaviour
{
    public float shake_radius = 2f;
    public float shake_speed = 20f;

    // for world space
    private Vector3 prev_position = new Vector3();
    private Vector3 current_shake_position;
    private Vector3 centerPoint = new Vector3();

    // for screenspace
    bool isScreenSpace = true;
    private float curr_left, curr_top;
    private float prev_left, prev_top;
    private float center_left, center_top;


    float t = 0.0f;
    public bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            if (isScreenSpace)
            {
                Shake_SS();
            }
            else
            {
                Shake_WS();
            }
        }
    }

    private void Shake_SS()
    {
        t += shake_speed * Time.deltaTime;
        float new_left = Mathf.Lerp(prev_left, curr_left, t);
        float new_top = Mathf.Lerp(prev_top, curr_top, t);
        GetComponent<CharacterText>().left = new_left;
        GetComponent<CharacterText>().top = new_top;

        if (t >= 1f)
        {
            prev_left = curr_left; prev_top = curr_top;
            curr_top = center_top + Random.Range(-shake_radius, shake_radius);
            curr_left = center_left + Random.Range(-shake_radius, shake_radius);
            t = 0.0f;
        }
    }

    private void Shake_WS()
    {
        t += shake_speed * Time.deltaTime;
        float new_pos_x = Mathf.Lerp(prev_position.x, current_shake_position.x, t);
        float new_pos_y = Mathf.Lerp(prev_position.y, current_shake_position.y, t);
        GetComponent<RectTransform>().position = new Vector3(new_pos_x, new_pos_y, centerPoint.z);

        if (t >= 1f)
        {
            prev_position = current_shake_position;
            current_shake_position = centerPoint + new Vector3(Random.Range(-shake_radius, shake_radius), Random.Range(-shake_radius, shake_radius), centerPoint.z);
            t = 0.0f;
        }
    }

    public void StartShake(Vector3 startPos)
    {
        isShaking = true; isScreenSpace = false;
        centerPoint = startPos;
        prev_position = startPos;
        current_shake_position = centerPoint + new Vector3(Random.Range(-shake_radius, shake_radius), Random.Range(-shake_radius, shake_radius), startPos.z);
    }

    public void StartShake(Vector3 startPos, float charOffset)
    {
        GetComponent<CharacterText>().anchorMin = startPos.x;
        GetComponent<CharacterText>().left = charOffset;
        isShaking = true; isScreenSpace = true;
        curr_top = GetComponent<CharacterText>().top + Random.Range(-shake_radius, shake_radius);
        curr_left = charOffset + Random.Range(-shake_radius, shake_radius);
        center_left = GetComponent<CharacterText>().left;
        center_top = GetComponent<CharacterText>().top;
    }

    public void UpdateCenterPos(Vector3 newPos)
    {
        centerPoint = newPos;
    }
}
