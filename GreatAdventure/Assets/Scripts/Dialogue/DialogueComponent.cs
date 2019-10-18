using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueComponent : MonoBehaviour
{
    public static DialogueComponent currentActiveDialogue = null;

    public DialogueAsset m_dialogueAsset; // holds asset data from editor
    int m_currentDialogueIndex;
    TextDisplay m_textDisplay;
    bool nextDialogue = true;
    bool hasPlayerOptions = false;
    int playerSelectedOption = 0;
    bool isActive = false;
    bool canChangeOption = true;
    bool optionChosen = false;

    // Start is called before the first frame update
    void Start()
    {
        m_dialogueAsset.LoadRuntimeSaveData();
        m_currentDialogueIndex = m_dialogueAsset.m_startIndex;
        m_textDisplay = GetComponent<TextDisplay>();

        if (m_dialogueAsset.m_isActiveDialogue)
        {
            m_textDisplay.SetAsActiveDialogue();
        }
        else
        {
            m_textDisplay.SetAsPassiveDialogue(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            if (m_dialogueAsset.m_isActiveDialogue)
            {
                UpdateActiveDialogue();
            }
            else
            {
                UpdatePassiveDialogue();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isActive == true)
        {
            if (!m_dialogueAsset.m_isActiveDialogue)
            {
                DialogueData dialogue;
                m_dialogueAsset.m_dialogueData.TryGetValue(m_currentDialogueIndex, out dialogue);
                CharacterComponent characterComp;
                m_dialogueAsset.m_characterData.TryGetValue(dialogue.characterName, out characterComp);
                Vector3 dialoguePosition = characterComp.GetCurrentOffset();

                //update current dialogue text positions with correct offset positions.
                m_textDisplay.UpdateCurrentDisplay(dialogue.dialogueText, dialoguePosition);
            }
        }
    }

    private void UpdateActiveDialogue()
    {
        DialogueData dialogue;
        m_dialogueAsset.m_dialogueData.TryGetValue(m_currentDialogueIndex, out dialogue);
        CharacterComponent characterComp;
        m_dialogueAsset.m_characterData.TryGetValue(dialogue.characterName, out characterComp);

        if (nextDialogue)
        {
            m_textDisplay.DisplayDialogueHeader(dialogue.characterName, characterComp.characterIcon);
            m_textDisplay.NewLine();

            // do all the actions for the current dialogue
            m_textDisplay.Display(dialogue.dialogueText);
            AddItemsToInventory(dialogue);
            UpdateQuests(dialogue);
            characterComp.OnCharacterTalk(dialogue.GetDialogueTextWithoutTags());

            // triggering dialogue game events
            if (dialogue.m_dialogueEvent != null)
            {
                dialogue.m_dialogueEvent.Invoke();
            }

            nextDialogue = false;
        }

        bool isNextDialogueButtonPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0);
        if (isNextDialogueButtonPressed && !m_textDisplay.iSDone())
        {
            m_textDisplay.StopTypeWriterEffect();
            m_textDisplay.ClearDisplay();
            m_textDisplay.DisplayDialogueHeader(dialogue.characterName, characterComp.characterIcon);
            m_textDisplay.NewLine();
            m_textDisplay.AddToDisplayImmediate(dialogue.dialogueText);
            return;
        }

        if (isNextDialogueButtonPressed && m_textDisplay.iSDone())
        {
            m_textDisplay.ClearDisplay();

            // next dialogue is not branching
            if (dialogue.m_nextDialogueData.Count == 1)
            {
                m_currentDialogueIndex = dialogue.m_nextDialogueData[0];
                hasPlayerOptions = false;
                nextDialogue = true;
            }

            // next dialogue is branching
            else if (dialogue.m_nextDialogueData.Count > 1)
            {
                if (!dialogue.isConditionalBranching)
                {
                    // display all branching options
                    DisplayDialogueOptions(dialogue.m_nextDialogueData);
                }
                else
                {
                    m_currentDialogueIndex = CheckDialogueConditions(dialogue);
                    nextDialogue = true;
                }
            }

            // branching option chosen
            if (optionChosen == true)
            {
                m_currentDialogueIndex = dialogue.m_nextDialogueData[playerSelectedOption];
                hasPlayerOptions = false;
                nextDialogue = true;
                m_textDisplay.ClearDisplay();
                optionChosen = false;
                playerSelectedOption = 0;
            }

            // dialogue is now complete
            if (dialogue.m_nextDialogueData.Count == 0)
            {
                isActive = false;
                currentActiveDialogue = null;
            }
        }
        // selecting player option
        else if (hasPlayerOptions)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            if (verticalInput != 0 && canChangeOption)
            {
                canChangeOption = false;
                StartCoroutine(ChangePlayerOption(dialogue.m_nextDialogueData.Count, verticalInput));
                DisplayDialogueOptions(dialogue.m_nextDialogueData);
            }
            optionChosen = true;
        }
    }

    private void AddItemsToInventory(DialogueData dialogue)
    {
        foreach(Item item in dialogue.itemsToGive)
        {
            if (!Inventory.HasItem(item.itemName))
                Inventory.AddItem(item.itemName, item);
        }
    }

    private bool CheckItemsInInventory(DialogueData dialogue)
    {
        bool hasItem = true;
        foreach (Item item in dialogue.itemsToCheck)
        {
            hasItem &= Inventory.HasItem((item.itemName));
        }
        return hasItem;
    }

    private void UpdateQuests(DialogueData dialogue)
    {
        foreach (Quest quest in dialogue.questsToAdd)
        {
            if (!ActiveQuests.HasQuest(quest.questName))
                ActiveQuests.AddQuest(quest.questName, quest);
        }

        foreach (Quest quest in dialogue.questsToComplete)
        {
            if (ActiveQuests.HasQuest(quest.questName))
                ActiveQuests.MarkQuestAsComplete(quest.questName);
        }
    }

    private bool CheckRequiredQuests(DialogueData dialogue)
    {
        bool hasQuest = true;
        foreach(Quest quest in dialogue.questsRequired)
        {
            hasQuest &= ActiveQuests.HasQuest(quest.questName);
        }
        return hasQuest;
    }

    private bool CheckCompletedQuests(DialogueData dialogue)
    {
        bool areQuestsComplete = true;
        foreach (Quest quest in dialogue.questsCompleted)
        {
            areQuestsComplete &= ActiveQuests.IsQuestComplete(quest.questName);
        }
        return areQuestsComplete;
    }

    private int CheckDialogueConditions(DialogueData dialogue)
    {
        bool condition = true;

        // checking items
        if (dialogue.itemsToCheck.Count > 0)
        {
            condition &= CheckItemsInInventory(dialogue);
        }

        // checking quests that need to be aquired
        if (dialogue.questsRequired.Count > 0)
        {
            condition &= CheckRequiredQuests(dialogue);
        }

        // checking quests that need to be completed
        if (dialogue.questsCompleted.Count > 0)
        {
            condition &= CheckCompletedQuests(dialogue);
        }

        int conditionIndex = condition ? 1 : 0; // it didnt allow me to cast bool to int /shrug
        return dialogue.m_nextDialogueData[conditionIndex];
    }

    private void UpdatePassiveDialogue()
    {
        DialogueData dialogue;
        m_dialogueAsset.m_dialogueData.TryGetValue(m_currentDialogueIndex, out dialogue);
        CharacterComponent characterComp;
        m_dialogueAsset.m_characterData.TryGetValue(dialogue.characterName, out characterComp);
        Vector3 dialoguePosition = characterComp.GetCurrentOffset();

        if (nextDialogue)
        {
            // do all the actions for the current dialogue
            m_textDisplay.Display(dialogue.dialogueText, dialoguePosition);
            AddItemsToInventory(dialogue);
            UpdateQuests(dialogue);
            characterComp.OnCharacterTalk(dialogue.GetDialogueTextWithoutTags());

            // triggering dialogue game events
            if (dialogue.m_dialogueEvent != null)
            {
                dialogue.m_dialogueEvent.Invoke();
            }

            nextDialogue = false;
        }


        bool isNextDialogueButtonPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0);
        if (isNextDialogueButtonPressed && !m_textDisplay.iSDone())
        {
            m_textDisplay.StopTypeWriterEffect();
            m_textDisplay.ClearDisplay();
            m_textDisplay.AddToDisplayImmediate(dialogue.dialogueText, dialoguePosition);
            return;
        }
        if (isNextDialogueButtonPressed && m_textDisplay.iSDone())
        {
            m_textDisplay.ClearDisplay();

            // triggering dialogue game events
            if (dialogue.m_dialogueEvent != null)
            {
                dialogue.m_dialogueEvent.Invoke();
            }

            // next dialogue is not branching
            if (dialogue.m_nextDialogueData.Count == 1)
            {
                m_currentDialogueIndex = dialogue.m_nextDialogueData[0];
                hasPlayerOptions = false;
                nextDialogue = true;
            }
            // next dialogue is branching
            else if (dialogue.m_nextDialogueData.Count > 1)
            {
                if (dialogue.isConditionalBranching)
                {
                    m_currentDialogueIndex = CheckDialogueConditions(dialogue);
                    nextDialogue = true;
                }
            }

            // dialogue is now complete
            if (dialogue.m_nextDialogueData.Count == 0)
            {
                isActive = false;
                currentActiveDialogue = null;
            }
        }
    }

    IEnumerator ChangePlayerOption(int optionCount, float verticalInput)
    {
        if (verticalInput > 0)
        {
            playerSelectedOption = (playerSelectedOption > 0) ? playerSelectedOption - 1 : optionCount - 1;
        }
        else if (verticalInput < 0)
        {
            playerSelectedOption = (playerSelectedOption + 1 >= optionCount) ? 0 : playerSelectedOption + 1;
        }
        yield return new WaitForSeconds(0.2f);
        canChangeOption = true;
    }

    public void StartDialogue()
    {
        if (!isActive)
        {
            isActive = true;
            nextDialogue = true;
            m_currentDialogueIndex = m_dialogueAsset.m_startIndex;
            currentActiveDialogue = this;
        }
    }

    public void StopDialogue()
    {
        isActive = false;
        currentActiveDialogue = null;
    }

    private void DisplayDialogueOptions(List<int> next_dialogue_list)
    {
        m_textDisplay.ClearDisplay();
        //header details
        DialogueData header_dialogue;
        m_dialogueAsset.m_dialogueData.TryGetValue(next_dialogue_list[0], out header_dialogue);
        CharacterComponent characterComp;
        m_dialogueAsset.m_characterData.TryGetValue(header_dialogue.characterName, out characterComp);
        m_textDisplay.DisplayDialogueHeader(header_dialogue.characterName, characterComp.characterIcon);
        m_textDisplay.NewLine();

        // displaying text for each dialogue option
        int drawIndex = 0;
        foreach (int dialogue_index in next_dialogue_list)
        {
            DialogueData dialogue;
            m_dialogueAsset.m_dialogueData.TryGetValue(dialogue_index, out dialogue);
            if (dialogue != null)
            {
                if (dialogue.previewDialogueText != "")
                {
                    if (drawIndex == playerSelectedOption)
                        m_textDisplay.AddBoldTextToDisplayImmediate(dialogue.previewDialogueText);
                    else
                        m_textDisplay.AddToDisplayImmediate(dialogue.previewDialogueText);
                }
                else
                {
                    if (drawIndex == playerSelectedOption)
                        m_textDisplay.AddBoldTextToDisplayImmediate(dialogue.dialogueText);
                    else
                        m_textDisplay.AddToDisplayImmediate(dialogue.dialogueText);
                }
                m_textDisplay.NewLine();
            }
            ++drawIndex;
        }

        hasPlayerOptions = true;
        nextDialogue = false;
    }
}
