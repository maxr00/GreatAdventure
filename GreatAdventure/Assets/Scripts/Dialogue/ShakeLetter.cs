using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeLetter : MonoBehaviour
{
    public float shake_radius = 2f;
    public float shake_speed = 20f;
    private Vector3 prev_position = new Vector3();
    private Vector3 current_shake_position;
    private Vector3 centerPoint = new Vector3();
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
    }

    public void StartShake(Vector3 startPos)
    {
        isShaking = true;
        centerPoint = startPos;
        prev_position = startPos;
        current_shake_position = centerPoint + new Vector3(Random.Range(-shake_radius, shake_radius), Random.Range(-shake_radius, shake_radius), startPos.z);
    }

    public void UpdateCenterPos(Vector3 newPos)
    {
        centerPoint = newPos;
    }
}
