using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefChaseManager : MonoBehaviour
{
    public GameObject thief;
    public EnterCar car;
    public DialogueComponent documentStoreWorker;

    public UnityEngine.UI.RawImage fadeToBlack;

    public void SpawnCar()
    {
        car.gameObject.SetActive(true);
    }

    public void StartThiefChase()
    {
        thief.SetActive(true);
        car.GetComponent<Rigidbody>().isKinematic = false;
        car.Enter();
        fadeToBlack.color = new Color(0, 0, 0, 0);
    }

    public void EndThiefChase()
    {
        thief.GetComponent<ThiefMovement>().enabled = false;
        fadeToBlack.enabled = true;
        StartCoroutine(endChaseFade());
    }

    IEnumerator endChaseFade()
    {
        float a = 0;
        while(a < 1)
        {
            a += 0.01f;
            fadeToBlack.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
        }

        thief.SetActive(false);
        car.Exit();
        ActiveQuests.MarkQuestAsComplete("CatchThief");
        documentStoreWorker.StartDialogue();

        yield return new WaitForSeconds(1.3f);

        while (a > 0)
        {
            a -= 0.01f;
            fadeToBlack.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
