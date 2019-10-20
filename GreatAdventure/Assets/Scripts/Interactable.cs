using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Range satisfied by entering trigger collider, or using the 'range' variable")]

    public float range = 0;
    public bool activateOnEnter = false;


    // Unity is dumb and puts headers in the wrong order
    [Header("isDialogue will trigger dialogue to start")]
    public bool isDialogue;
    [Header("Other functions to call on activation:")]
    public UnityEvent activationFunc = new UnityEvent();

    PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        if(player == null)
        {
            Debug.Log("No player found. Put the 'Player' tag on the player!");
        }
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= range)
        {
            if(activateOnEnter)
            {
                Activate();
            }
            else
            {
                if (player.interacting)
                {
                    Activate();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (activateOnEnter)
            {
                Activate();
            }
            else
            {
                if (player.interacting)
                {
                    Activate();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player.interacting)
            {
                Activate();
            }
        }
    }

    void Activate()
    {
        if(isDialogue)
        {
            GetComponent<DialogueComponent>().StartDialogue();
        }

        activationFunc.Invoke();
    }
}
