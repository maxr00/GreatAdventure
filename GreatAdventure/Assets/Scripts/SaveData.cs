using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{

    public float[] position;
    public int itemCount = 0;
    public string[] playerItems;
    public int questCount = 0;
    public string[] playerAcquiredQuests;
    public int questCompleteCount = 0;
    public string[] playerCompletedQuests;

    public SaveData(Vector3 playerPosition)
    {
        // saving position
        position = new float[3];
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        // saving player items
        itemCount = Inventory.GetCurrentItems().Count;
        playerItems = new string[itemCount];
        int item_index = 0;
        foreach (KeyValuePair<string, Item> item_pair in Inventory.GetCurrentItems())
        {
            playerItems[item_index] = item_pair.Key;
            item_index++;
        }

        // saving player quests acquired and quests complete
        questCount = ActiveQuests.GetActiveQuests().Count;
        playerAcquiredQuests = new string[questCount];
        playerCompletedQuests = new string[questCount];
        int quest_index = 0;
        int quest_complete_index = 0;
        foreach (KeyValuePair<string, Quest> quest_pair in ActiveQuests.GetActiveQuests())
        {
            if (quest_pair.Value.isComplete)
            {
                playerCompletedQuests[quest_complete_index] = quest_pair.Key;
                quest_complete_index++;
            }

            playerAcquiredQuests[quest_index] = quest_pair.Key;
            quest_index++;
        }
        questCompleteCount = quest_complete_index;
    }
}
