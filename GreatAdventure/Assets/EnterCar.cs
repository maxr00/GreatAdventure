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
        playerTarget.GetComponent<SeeThroughWall>().enabled = false;

        player.allowMovement = false;

        entered = true;
    }

    public void Exit()
    {
        GetComponent<CarControls>().enabled = false;
        cam.currState = CameraSwitcher.State.GAMEPLAY;

        oldParent = null;

        playerTarget.GetComponent<SeeThroughWall>().enabled = true;

        player.allowMovement = true;

        entered = false;
    }

    private void Update()
    {
        /* Debug:
        if (entered)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Exit();
            }
        }
        */
    }
}
