using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    CinemachineBrain brain;

    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera dialogueCamera;

    public enum State { GAMEPLAY, DIALOGUE };
    public State currState;

    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        if(currState == State.GAMEPLAY)
        {
            if (DialogueComponent.currentActiveDialogue != null)
            {
                currState = State.DIALOGUE;
                defaultCamera.gameObject.SetActive(false);
                dialogueCamera.gameObject.SetActive(true);
            }
        }
        else if(currState == State.DIALOGUE)
        {
            if (DialogueComponent.currentActiveDialogue == null)
            {
                currState = State.GAMEPLAY;
                defaultCamera.gameObject.SetActive(true);
                dialogueCamera.gameObject.SetActive(false);
            }
        }
    }
}
