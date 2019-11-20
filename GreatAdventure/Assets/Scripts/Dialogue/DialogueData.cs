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
    public int branchingIndex = 0;
    public string characterName = "";
    public string dialogueText = "";
    public string previewDialogueText = "";

    // character information
    public int characterSpeakingIndex = 0;
    public List<CharacterComponent.Emotion> characterEmotions = new List<CharacterComponent.Emotion>();

    // item data for dialogue
    public List<Item> itemsToGive = new List<Item>();
    public List<Item> itemsToRemove = new List<Item>();
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
    public bool m_isEventNode = false;

    // dialogue events
    public List<GameObject> eventObjects = new List<GameObject>();
    public List<string> eventObjectNames = new List<string>();
    public List<string> eventFunctions = new List<string>();

    public List<int> m_nextDialogueData;

    // populated at runtime
    public Dictionary<string, CharacterComponent.Emotion> emotionsDictionary;

    public DialogueData Copy()
    {
        DialogueData copyDialogue = CreateInstance<DialogueData>();
        copyDialogue.characterName = characterName;
        copyDialogue.dialogueText = dialogueText;
        copyDialogue.previewDialogueText = previewDialogueText;

        copyDialogue.isConditionalBranching = isConditionalBranching;
        copyDialogue.characterSpeakingIndex = characterSpeakingIndex;

        // copying all lists
        copyDialogue.characterEmotions = new List<CharacterComponent.Emotion>();
        copyDialogue.characterEmotions.AddRange(characterEmotions);
        copyDialogue.itemsToGive = new List<Item>();
        copyDialogue.itemsToGive.AddRange(itemsToGive);
        copyDialogue.itemsToRemove = new List<Item>();
        copyDialogue.itemsToRemove.AddRange(itemsToRemove);
        copyDialogue.questsToAdd = new List<Quest>();
        copyDialogue.questsToAdd.AddRange(questsToAdd);
        copyDialogue.questsToComplete = new List<Quest>();
        copyDialogue.questsToComplete.AddRange(questsToComplete);

        //conditional
        copyDialogue.itemsToCheck = new List<Item>();
        copyDialogue.itemsToCheck.AddRange(itemsToCheck);
        copyDialogue.questsRequired = new List<Quest>();
        copyDialogue.questsRequired.AddRange(questsRequired);
        copyDialogue.questsCompleted = new List<Quest>();
        copyDialogue.questsCompleted.AddRange(questsCompleted);

        // events
        copyDialogue.eventObjects = new List<GameObject>();
        copyDialogue.eventObjects.AddRange(eventObjects);
        copyDialogue.eventFunctions = new List<string>();
        copyDialogue.eventFunctions.AddRange(eventFunctions);
        return copyDialogue;
    }

    public string GetDialogueTextWithoutTags()
    {
        string tag_pattern = "<[^>]+>";
        return Regex.Replace(dialogueText, tag_pattern, "");
    }
}
