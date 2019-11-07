using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 0.7f;
    public Vector3 offset = new Vector3(0, 0, 0);
    public float distance = 10;

    public float zoomSpeed = 1;
    public float distanceMin = 1;
    public float distanceMax = 10;

    public float xRotationLimit = 88;

    private Vector3 lastMousePos;

    // Start is called before the first frame update
    void Start()
    {
        lastMousePos = Input.mousePosition;

        transform.localPosition = offset;
        transform.forward = transform.parent.forward;
        transform.Rotate(20, 0, 0);
        transform.Translate(new Vector3(0, 0, -distance));

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance -= Input.mouseScrollDelta.y * zoomSpeed;
        distance = Mathf.Max(distanceMin, distance);
        distance = Mathf.Min(distanceMax, distance);
        
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            var now = Input.mousePosition;
            var delta = now - lastMousePos;
        
            transform.Rotate(Vector3.up, delta.x * speed); // Horizontal rotation

            float newX = transform.eulerAngles.x + -delta.y * speed;
            if (newX <= xRotationLimit || newX >= -xRotationLimit + 360)
            {
                transform.Rotate(-delta.y * speed, 0, 0); // Vertical rotation
            }

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0); // Don't make everyone sick rotation

            transform.localPosition = offset;
            transform.Translate(new Vector3(0, 0, -distance));

            lastMousePos = now;
        }
        else
        {
            if(Input.GetMouseButtonDown(2))
            {
                transform.localPosition = offset;
                transform.forward = transform.parent.forward;
                transform.Rotate(20, 0, 0);
                transform.Translate(new Vector3(0, 0, -distance));
            }

            lastMousePos = Input.mousePosition;
        }
    }
}
