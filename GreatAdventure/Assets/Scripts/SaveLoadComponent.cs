using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public LoadData loadData;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SaveData data = SaveSystem.LoadGame();

        //load save position
        Vector3 save_position;
        save_position.x = data.position[0];
        save_position.y = data.position[1];
        save_position.z = data.position[2];
        player.transform.position = save_position;

        //load inventory
        loadData.LoadInventory(data.playerItems, data.itemCount);

        //load quests
        loadData.LoadQuests(data.playerAcquiredQuests, data.questCount);
        loadData.MarkQuestsAsComplete(data.playerCompletedQuests, data.questCompleteCount);
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGame(player.transform.position);
    }
}
