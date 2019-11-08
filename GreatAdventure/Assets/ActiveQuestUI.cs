using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveQuestUI : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var quests = ActiveQuests.GetActiveQuests();

        text.text = "";
        foreach (var quest in quests.Values)
        {
            if(quest.parentQuest == null || !quest.parentQuest.isComplete)
            {
                Display(quest);
            }
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
