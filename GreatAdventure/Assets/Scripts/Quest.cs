using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
    public string questName;

    public bool isComplete;

    public List<Quest> subQuests;
    public Quest parentQuest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddQuestToActiveQuests()
    {
        ActiveQuests.AddQuest(questName, this);
    }

    public void RemoveItemFromInventory()
    {
        ActiveQuests.RemoveQuest(questName);
    }
}
