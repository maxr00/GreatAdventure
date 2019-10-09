using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAsset : ScriptableObject
{
    // things i guesss..
    public Dictionary<int, DialogueData> m_dialogueData; // what the dialogue system will use
    public Dictionary<string, CharacterComponent> m_characterData; // what the dialogue system will use.
    public int m_startIndex;
    public List<DialogueData> m_runtimeBuiltData; // for runtime stuff
    [HideInInspector]public string m_assetData = ""; // for editor stuff
    public List<string> m_charactersInvolvedStrings;

    // shown in editor
    [HideInInspector] public List<GameObject> m_CharactersInvoled;
    public bool m_isActiveDialogue = true;

    public DialogueAsset()
    {
        m_dialogueData = new Dictionary<int, DialogueData>();
        m_runtimeBuiltData = new List<DialogueData>();
        m_CharactersInvoled = new List<GameObject>();
        
    }
    private void OnEnable()
    {
    }

    // edit here when dialogue data changes
    public void LoadRuntimeSaveData()
    {
        m_dialogueData = new Dictionary<int, DialogueData>();
        m_characterData = new Dictionary<string, CharacterComponent>();

        // load dialogue data (runtime)
        for (int dialogue_index = 0; dialogue_index < m_runtimeBuiltData.Count; ++dialogue_index)
        {
            DialogueData dialogue = m_runtimeBuiltData[dialogue_index];
            m_dialogueData.Add(dialogue.node_id, dialogue);
        }

        for (int gameob_index = 0; gameob_index < m_charactersInvolvedStrings.Count; ++gameob_index)
        {
            string go_name = m_charactersInvolvedStrings[gameob_index];
            m_characterData.Add(go_name, GameObject.Find(go_name).GetComponent<CharacterComponent>());
        }
    }

    public List<string> GetInvolvedCharacterStrings()
    {
        List<string> charNames = new List<string>();
        foreach (GameObject chars in m_CharactersInvoled)
        {
            if (chars != null)
                charNames.Add(chars.name);
        }
        return charNames;
    }

    public void LoadCharactersInvolvedGameObjects() // for editor only
    {
        m_CharactersInvoled = new List<GameObject>();
        if (m_charactersInvolvedStrings != null && m_charactersInvolvedStrings.Count >= 0)
        {
            for (int gameob_index = 0; gameob_index < m_charactersInvolvedStrings.Count; ++gameob_index)
            {
                string go_name = m_charactersInvolvedStrings[gameob_index];
                m_CharactersInvoled.Add(GameObject.Find(go_name));
            }
        }
        else
        {
            m_CharactersInvoled.Add(null);
        }
    }
}
