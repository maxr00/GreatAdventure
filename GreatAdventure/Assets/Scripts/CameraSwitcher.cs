using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    CinemachineBrain brain;

    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera dialogueCamera;
    public CinemachineVirtualCamera carCamera;

    public enum State { GAMEPLAY, DIALOGUE, CAR };
    public State currState;

    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        if(currState == State.GAMEPLAY)
        {
            defaultCamera.gameObject.SetActive(true);
            dialogueCamera.gameObject.SetActive(false);
            carCamera.gameObject.SetActive(false);
            if (DialogueComponent.currentActiveDialogue != null)
            {
                currState = State.DIALOGUE;
            }
        }
        else if(currState == State.DIALOGUE)
        {
            defaultCamera.gameObject.SetActive(false);
            dialogueCamera.gameObject.SetActive(true);
            carCamera.gameObject.SetActive(false);
            if (DialogueComponent.currentActiveDialogue == null)
            {
                currState = State.GAMEPLAY;
            }
        }
        else if(currState == State.CAR)
        {
            defaultCamera.gameObject.SetActive(false);
            dialogueCamera.gameObject.SetActive(false);
            carCamera.gameObject.SetActive(true);
        }
    }
}
