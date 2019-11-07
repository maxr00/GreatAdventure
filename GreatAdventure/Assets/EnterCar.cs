using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCar : MonoBehaviour
{
    public GameObject playerTarget;
    public PlayerController player;
    public CameraSwitcher cam;

    bool entered = false;
    Transform oldParent = null;

    public void Enter()
    {
        GetComponent<CarControls>().enabled = true;
        cam.currState = CameraSwitcher.State.CAR;

        oldParent = player.transform.parent;
        //playerTarget.transform.parent = transform;

        //playerTarget.transform.position = transform.position;

        player.allowMovement = false;
        //playerTarget.GetComponent<CameraFollow>().enabled = false;

        entered = true;
    }

    public void Exit()
    {
        GetComponent<CarControls>().enabled = false;
        cam.currState = CameraSwitcher.State.GAMEPLAY;

        //playerTarget.transform.parent = oldParent;
        oldParent = null;

        player.allowMovement = true;
        //playerTarget.GetComponent<CameraFollow>().enabled = true;

        //playerTarget.transform.position = player.transform.position;

        entered = false;
    }

    private void Update()
    {
        if (entered)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Exit();
            }
        }
    }
}
