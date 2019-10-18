using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadData : ScriptableObject
{
    public Dictionary<string, Item> items;
    public Dictionary<string, Quest> quests;

    public List<Item> allItems;
    public List<Quest> allQuests;

    public void LoadDataIntoDictionaries()
    {
        items = new Dictionary<string, Item>();
        foreach(Item item in allItems)
        {
            items.Add(item.itemName, item);
        }

        quests = new Dictionary<string, Quest>();
        foreach(Quest quest in allQuests)
        {
            quests.Add(quest.questName, quest);
        }
    }

    public void OnEnable()
    {
        LoadDataIntoDictionaries();
    }

    public void LoadInventory(string[] item_names, int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            Item itemToAdd;
            items.TryGetValue(item_names[i], out itemToAdd);
            Inventory.AddItem(itemToAdd.itemName, itemToAdd);
        }
    }

    public void LoadQuests(string[] questsAcquired, int questCount)
    {
        for (int i = 0; i < questCount; i++)
        {
            Quest questToAdd;
            quests.TryGetValue(questsAcquired[i], out questToAdd);
            ActiveQuests.AddQuest(questToAdd.questName, questToAdd);
        }
    }

    public void MarkQuestsAsComplete(string[] completedQuests, int completeCount)
    {
        for (int i = 0; i < completeCount; i++)
        {
            ActiveQuests.MarkQuestAsComplete(completedQuests[i]);
        }
    }
}

