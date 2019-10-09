using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActiveQuests
{
    // another way to identify quests other than name?
    private static Dictionary<string, Quest> m_activeQuests = new Dictionary<string, Quest>();

    public static void AddQuest(string questName, Quest quest)
    {
        quest.isComplete = false;
        m_activeQuests.Add(questName, quest);
    }

    public static void RemoveQuest(string questName)
    {
        m_activeQuests.Remove(questName);
    }

    public static Quest GetQuestFromName(string questName)
    {
        Quest quest;
        m_activeQuests.TryGetValue(questName, out quest);
        return quest;
    }

    public static bool HasQuest(string questName)
    {
        return m_activeQuests.ContainsKey(questName);
    }

    public static bool IsQuestComplete(string questName)
    {
       return GetQuestFromName(questName).isComplete;        
    }

    public static void MarkQuestAsComplete(string questName, Quest quest)
    {
        GetQuestFromName(questName).isComplete = true;
        Debug.Log(questName + " is complete");
    }

    public static Dictionary<string, Quest> GetActiveQuests()
    {
        return m_activeQuests;
    }
}
