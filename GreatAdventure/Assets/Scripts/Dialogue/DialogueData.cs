using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueData : ScriptableObject
{
    public int node_id;
    public bool m_isStartNode;
    public int branchingIndex = -1;
    public string characterName = "";
    public string dialogueText = "";
    public string previewDialogueText = "";

    // character information
    public int characterSpeakingIndex = 0;
    public CharacterComponent.Emotion emotion;

    // item data for dialogue
    public List<Item> itemsToGive = new List<Item>();
    // quest data for dialogue
    public List<Quest> questsToAdd = new List<Quest>();
    public List<Quest> questsToComplete = new List<Quest>();

    // conditional branching nodes
    public bool isConditionalBranching = false;
    public List<Item> itemsToCheck = new List<Item>();
    // probably quests to check here as well
    public List<Quest> questsRequired = new List<Quest>();
    public List<Quest> questsCompleted = new List<Quest>();

    public bool m_isBranching;
    public UnityEvent m_dialogueEvent;

    public List<int> m_nextDialogueData;

    public DialogueData Copy()
    {
        DialogueData copyDialogue = CreateInstance<DialogueData>();
        copyDialogue.characterName = characterName;
        copyDialogue.dialogueText = dialogueText;
        copyDialogue.previewDialogueText = previewDialogueText;
        return copyDialogue;
    }

    public string GetDialogueTextWithoutTags()
    {
        string tag_pattern = "<[^>]+>";
        return Regex.Replace(dialogueText, tag_pattern, "");
    }
}
