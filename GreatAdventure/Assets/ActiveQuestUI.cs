using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveQuestUI : MonoBehaviour
{
    RectTransform rt;

    public TextMeshProUGUI text;

    Vector3 posStart;
    public Vector3 offscreenOffset;

    public Quest debugStartQuest; ///

    int numQuests = 0;
    int numCompleted = 0;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        posStart = rt.position;

        // DEBUG STARTING QUEST
        ActiveQuests.AddQuest(debugStartQuest.name, debugStartQuest);
    }

    void Update()
    {
        var quests = ActiveQuests.GetActiveQuests();

        int completed = 0;
        foreach (var quest in quests.Values)
        {
            if (quest.isComplete)
                completed++;
        }

        if (numCompleted != completed || numQuests != quests.Count)
        {
            numCompleted = completed;
            numQuests = quests.Count;
            StartCoroutine(UpdateJournal());
        }
    }

    IEnumerator UpdateJournal()
    {
        float t = 0;

        while(t < 1)
        {
            t += 0.05f;
            rt.position = Vector3.Lerp(posStart, posStart + offscreenOffset, t);
            yield return new WaitForSeconds(0.03f);
        }

        var quests = ActiveQuests.GetActiveQuests();
        text.text = "";
        foreach (var quest in quests.Values)
        {
            if (quest.parentQuest == null || !quest.parentQuest.isComplete)
            {
                Display(quest);
            }
        }

        while (t > 0)
        {
            t -= 0.05f;
            rt.position = Vector3.Lerp(posStart, posStart + offscreenOffset, t);
            yield return new WaitForSeconds(0.03f);
        }
    }

    void Display(Quest q)
    {
        if (q.isComplete)
        {
            text.text += "<s>>" + q.questName + "</s>\n";
        }
        else
        {
            text.text += ">" + q.questName + "\n";
        }
    }

}
