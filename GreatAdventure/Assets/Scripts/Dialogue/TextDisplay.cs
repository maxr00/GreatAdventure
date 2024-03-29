﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    List<GameObject> textChars;
    List<TextEffect> totalTextModifiers;
    public TextPool textPool;

    int textCharsStartIndex = 0;

    public GameObject CharacterIconPrefab;
    public GameObject CharacterNamePrefab;
    public Vector3 MainDialogueStart = new Vector3(0.1f, 0.25f, 0);
    public Vector3 OptionDialogueStart = new Vector3(0.22f, 0.28f, 0);
    public float newLineOffset = 0.05f;
    public float optionOffset = 0.1f;

    public Vector2 anchoroffset = new Vector2(0.5f, 0.25f);
    //public Vector3 header_start_pos = new Vector3(0.25f, 0.39f, 0);
    public float header_xOffset = 75;
    public float icon_xOffset = 250;

    private static GameObject characterIcon = null;
    private static GameObject characterName = null;

    //dialogue bubble details
    public GameObject DialogueBubble;
    public GameObject DialogueOptionBubble;
    private static GameObject[] DialogueOptionBubbles = new GameObject[3];

    private Vector3 startPos = new Vector3();
    private bool done = true;
    private string tag_pattern = "<[^>]+>";
    private float default_char_delay = 0.05f;
    public float current_char_delay = 0.05f;
    private float default_pause_time = 1.0f;
    private bool isPaused = false;
    private float pause_time = 1.0f;
    private float currentLineOffset = 0.0f;
    private bool stopTypewriterEffect = false;
    private bool isActiveDialogue = true;

    private void Start()
    {
        textChars = new List<GameObject>();
        totalTextModifiers = new List<TextEffect>();
        textCharsStartIndex = 0;
        if (characterIcon == null)
            characterIcon = GameObject.Instantiate(CharacterIconPrefab, textPool.GetComponent<Transform>());
        if (characterName == null)
            characterName = GameObject.Instantiate(CharacterNamePrefab, textPool.GetComponent<Transform>());
        characterIcon.GetComponent<Image>().enabled = false;
        characterName.SetActive(false);
        startPos = MainDialogueStart;


        if (DialogueOptionBubble != null)
        {
            for (int i = 0; i < 3; ++i)
            {
                DialogueOptionBubbles[i] = GameObject.Instantiate(DialogueOptionBubble, textPool.GetComponent<Transform>());
                Vector2 defaultAnchMin = DialogueOptionBubbles[i].GetComponent<RectTransform>().anchorMin;
                Vector2 defaultAnchMax = DialogueOptionBubbles[i].GetComponent<RectTransform>().anchorMax;
                DialogueOptionBubbles[i].GetComponent<RectTransform>().anchorMin = defaultAnchMin + new Vector2(0, (-0.1f * i));
                DialogueOptionBubbles[i].GetComponent<RectTransform>().anchorMax = defaultAnchMax + new Vector2(0, (-0.1f * i));
                DialogueOptionBubbles[i].SetActive(false);
            }
        }
    }

    public void SetAsActiveDialogue()
    {
        textPool.GetComponent<RectTransform>().position = new Vector3(300, 300, 0);
        textPool.GetComponent<RectTransform>().sizeDelta = new Vector2(598, 597);
        textPool.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        isActiveDialogue = true;
    }

    public void SetAsPassiveDialogue(Vector3 obj_pos)
    {
        newLineOffset = 1.2f;

        textPool.GetComponent<RectTransform>().position = obj_pos;
        textPool.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
        textPool.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;

        MainDialogueStart = obj_pos + new Vector3(1, 1, -1);
        startPos = MainDialogueStart;
        isActiveDialogue = false;
    }

    public void UpdateCurrentDisplay(string text, Vector3 newPosition)
    {
        string displayText = Regex.Replace(text, tag_pattern, "");
        float localLineOffset = 0;
        for (int i = 0; i < textChars.Count; ++i)
        {
            textChars[i].GetComponent<RectTransform>().position = newPosition + new Vector3(localLineOffset, 0, 0);
            textChars[i].GetComponent<ShakeLetter>().UpdateCenterPos(textChars[i].GetComponent<RectTransform>().position);
            localLineOffset += GetCharacterWidth(textChars[i].GetComponent<TextMeshProUGUI>());
        }
    }

    public void UpdatePassiveBubble(Vector3 newPosition)
    {
        DialogueBubble.transform.position = newPosition;
    }

    public void DisplayingOptions()
    {
        startPos = OptionDialogueStart;
    }

    public void StopDisplayingOptions()
    {
        startPos = MainDialogueStart;
    }

    public void NewOption()
    {
        currentLineOffset = 0.0f;
        startPos.y -= optionOffset;
    }

    public void StopTypeWriterEffect()
    {
        stopTypewriterEffect = true;
    }

    public void Display(string text)
    {
        stopTypewriterEffect = false;
        done = false;

        float pixelOffset = -DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f;
        pixelOffset /= Camera.main.aspect;
        
        currentLineOffset = pixelOffset;
        current_char_delay = default_char_delay;
        PopulateModifierList(text);
        textCharsStartIndex = Mathf.Max(0, textChars.Count);
        string displayText = Regex.Replace(text, tag_pattern, "");
        textChars.AddRange(textPool.Claim(displayText.Length));
        ClearCharProperties(displayText);
        StartCoroutine(DisplayText(displayText));
    }

    public void Display(string text, Vector3 dialogueStart)
    {
        stopTypewriterEffect = false;
        startPos = dialogueStart;
        MainDialogueStart = dialogueStart;

        done = false;

        currentLineOffset = 0.0f;
        if (isActiveDialogue)
        {
            float pixelOffset = -DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f;
            pixelOffset /= Camera.main.aspect;

            currentLineOffset = pixelOffset;
        }

        PopulateModifierList(text);
        textCharsStartIndex = Mathf.Max(0, textChars.Count);
        string displayText = Regex.Replace(text, tag_pattern, "");
        textChars.AddRange(textPool.Claim(displayText.Length));
        ClearCharProperties(displayText);
        StartCoroutine(DisplayText(displayText));
    }

    public void DisplayActiveBubble(bool isActive)
    {
        DialogueBubble.gameObject.SetActive(isActive);
    }

    public void DisplayActiveOptionBubble(bool isActive, int optionCount)
    {
        if (isActive)
        {
            for (int i = 0; i < optionCount; ++i)
            {
                DialogueOptionBubbles[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                DialogueOptionBubbles[i].SetActive(false);
            }
        }
    }

    public void DisplayPassiveBubble(bool isActive, Vector3 position)
    {
        DialogueBubble.SetActive(isActive);

        DialogueBubble.transform.position = position;

    }

    public void DisplayDialogueHeader(string dialogue_header_text, Sprite header_icon)
    {
        if (header_icon != null)
        {
            characterIcon.GetComponent<Image>().enabled = true;
            characterIcon.GetComponent<Image>().sprite = header_icon;
            characterIcon.GetComponent<Image>().preserveAspect = true;
        }

        characterName.SetActive(true);
        characterName.GetComponent<TextMeshProUGUI>().text = "<b>" + dialogue_header_text + "</b>";
        characterName.GetComponent<TextMeshProUGUI>().fontSize = 40;
        characterName.GetComponent<TextMeshProUGUI>().color = Color.black;
        characterName.GetComponent<TextMeshProUGUI>().outlineColor = Color.white;
        characterName.GetComponent<TextMeshProUGUI>().outlineWidth = 10f;



        //PopulateModifierList(dialogue_header_text);

        //float pixelOffset = (-DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f) + header_xOffset;
        //pixelOffset /= Camera.main.aspect;

        //currentLineOffset = pixelOffset;
        //textCharsStartIndex = Mathf.Max(0, textChars.Count);
        //string displayText = Regex.Replace(dialogue_header_text, tag_pattern, "");
        //textChars.AddRange(textPool.Claim(displayText.Length));
        //ClearCharProperties(displayText);
        //DisplayTextImmediate(displayText, header_start_pos);
    }

    public void AddToDisplayImmediate(string text)
    {
        PopulateModifierList(text);

        float pixelOffset = -DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f;
        pixelOffset /= Camera.main.aspect;

        currentLineOffset = pixelOffset;
        textCharsStartIndex = Mathf.Max(0, textChars.Count);
        string displayText = Regex.Replace(text, tag_pattern, "");
        textChars.AddRange(textPool.Claim(displayText.Length));
        ClearCharProperties(displayText);
        DisplayTextImmediate(displayText);
        done = true;
    }

    public void AddToDisplayImmediate(string text, Vector3 dialogueStart)
    {
        startPos = dialogueStart;
        PopulateModifierList(text);
        currentLineOffset = 0.0f;

        if (isActiveDialogue)
        {
            float pixelOffset = -DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f;
            pixelOffset /= Camera.main.aspect;

            currentLineOffset = pixelOffset;
        }

        textCharsStartIndex = Mathf.Max(0, textChars.Count);
        string displayText = Regex.Replace(text, tag_pattern, "");
        textChars.AddRange(textPool.Claim(displayText.Length));
        ClearCharProperties(displayText);
        DisplayTextImmediate(displayText);
        done = true;
    }

    public void AddBoldTextToDisplayImmediate(string text)
    {
        PopulateModifierList(text);
        currentLineOffset = 0.0f;
        if (isActiveDialogue)
        {
            float pixelOffset = -DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f;
            pixelOffset /= Camera.main.aspect;

            currentLineOffset = pixelOffset;
        }

        textCharsStartIndex = Mathf.Max(0, textChars.Count);
        string displayText = Regex.Replace(text, tag_pattern, "");

        TextEffect boldEffect = new TextEffect();
        boldEffect.type = TextEffect.TextEffectType.kBold;
        boldEffect.start_tag = "<b>";
        boldEffect.end_tag = "</b>";
        boldEffect.hasEndTag = true;
        boldEffect.start_index = 0;
        boldEffect.end_index = displayText.Length - 1;
        totalTextModifiers.Add(boldEffect);

        textChars.AddRange(textPool.Claim(displayText.Length));
        ClearCharProperties(displayText);
        DisplayTextImmediate(displayText);
    }

    public void ClearDisplay()
    {
        currentLineOffset = 0.0f;

        if (isActiveDialogue)
        {
            float pixelOffset = -DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f;
            pixelOffset /= Camera.main.aspect;

            currentLineOffset = pixelOffset;
        }

        startPos = MainDialogueStart;
        textPool.Unclaim(textChars);
        textChars = new List<GameObject>();
        characterIcon.GetComponent<Image>().enabled = false;
        characterName.SetActive(false);
    }

    public void NewLine()
    {
        currentLineOffset = 0.0f;
        if (isActiveDialogue)
        {
            float pixelOffset = -DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f;
            pixelOffset /= Camera.main.aspect;

            currentLineOffset = pixelOffset;
        }

        startPos.y -= newLineOffset;
    }

    IEnumerator DisplayText(string displayText)
    {
        var chars = displayText.ToCharArray();
        bool word_wrap_nl = false;
        for (int i = 0; i < displayText.Length; i++)
        {
            if (word_wrap_nl && chars[i] == ' ')
            {
                word_wrap_nl = false;
                NewLine();
                continue;
            }

            if (chars[i] == '\n')
            {
                NewLine();
                continue;
            }
            if (isActiveDialogue)
            {
                textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>().text = GetStartCharTags(i) + chars[i].ToString() + GetEndCharTags(i);
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().left = currentLineOffset;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().anchorMin = anchoroffset.x;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().anchorMax = startPos.y;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().UpdateNow();
                ApplyTextModifiers(i, textChars[i + textCharsStartIndex]);
                currentLineOffset += GetCharacterWidth(textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>());

                // word wrap check
                float wordWrapOffset = (DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f) - 100;
                wordWrapOffset /= Camera.main.aspect;
                if (currentLineOffset >= wordWrapOffset)
                    word_wrap_nl = true;
            }
            else
            {
                textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>().text = GetStartCharTags(i) + chars[i].ToString() + GetEndCharTags(i);
                textChars[i + textCharsStartIndex].GetComponent<RectTransform>().position = startPos + new Vector3(currentLineOffset, 0, 0);
                ApplyTextModifiers(i, textChars[i + textCharsStartIndex]);
                currentLineOffset += GetCharacterWidth(textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>());
            }

            if (!isPaused)
            {
                yield return new WaitForSeconds(current_char_delay);
            }
            else
            {
                yield return new WaitForSeconds(pause_time);
                isPaused = false;
            }
            
            if (stopTypewriterEffect)
            {
                yield break;
            }
        }
        done = true;
        stopTypewriterEffect = false;
    }
    
    private void DisplayTextImmediate(string displayText)
    {
        var chars = displayText.ToCharArray();
        bool word_wrap_nl = false;
        for (int i = 0; i < displayText.Length; i++)
        {
            if (word_wrap_nl && chars[i] == ' ')
            {
                word_wrap_nl = false;
                NewLine();
                continue;
            }
            if (chars[i] == '\n')
            {
                NewLine();
                continue;
            }
            if (isActiveDialogue)
            {
                textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>().text = GetStartCharTags(i) + chars[i].ToString() + GetEndCharTags(i);
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().left = currentLineOffset;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().anchorMin = startPos.x;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().anchorMax = startPos.y;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().UpdateNow();
                ApplyTextModifiers(i, textChars[i + textCharsStartIndex]);
                currentLineOffset += GetCharacterWidth(textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>());

                // word wrap check
                float wordWrapOffset = (DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f) - 100;
                wordWrapOffset /= Camera.main.aspect;
                if (currentLineOffset >= wordWrapOffset)
                    word_wrap_nl = true;
            }
            else
            {
                textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>().text = GetStartCharTags(i) + chars[i].ToString() + GetEndCharTags(i);
                textChars[i + textCharsStartIndex].GetComponent<RectTransform>().position = startPos + new Vector3(currentLineOffset, 0, 0);
                ApplyTextModifiers(i, textChars[i + textCharsStartIndex]);
                currentLineOffset += GetCharacterWidth(textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>());
            }
        }
    }

    private void DisplayTextImmediate(string displayText, Vector3 startPos)
    {
        var chars = displayText.ToCharArray();
        bool word_wrap_nl = false;
        for (int i = 0; i < displayText.Length; i++)
        {
            if (word_wrap_nl && chars[i] == ' ')
            {
                word_wrap_nl = false;
                NewLine();
                continue;
            }
            if (isActiveDialogue)
            {
                textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>().text = GetStartCharTags(i) + chars[i].ToString() + GetEndCharTags(i);
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().left = currentLineOffset;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().anchorMin = startPos.x;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().anchorMax = startPos.y;
                textChars[i + textCharsStartIndex].GetComponent<CharacterText>().UpdateNow();
                ApplyTextModifiers(i, textChars[i + textCharsStartIndex]);
                currentLineOffset += GetCharacterWidth(textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>());

                // word wrap check
                float wordWrapOffset = (DialogueBubble.GetComponent<Image>().GetPixelAdjustedRect().width / 2.0f) - 100;
                wordWrapOffset /= Camera.main.aspect;
                if (currentLineOffset >= wordWrapOffset)
                    word_wrap_nl = true;
            }
            else
            {
                textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>().text = GetStartCharTags(i) + chars[i].ToString() + GetEndCharTags(i);
                textChars[i + textCharsStartIndex].GetComponent<RectTransform>().position = startPos + new Vector3(currentLineOffset, 0, 0);
                ApplyTextModifiers(i, textChars[i + textCharsStartIndex]);
                currentLineOffset += GetCharacterWidth(textChars[i + textCharsStartIndex].GetComponent<TextMeshProUGUI>());
            }
        }
    }

    public void DebugDisplayText()
    {
        string stringFromList = "";
        for (int i = 0; i < textChars.Count; ++i)
        {
            stringFromList += textChars[i].GetComponent<Text>().text;
        }
        Debug.Log(stringFromList);
    }

    private void ClearCharProperties(string displayText)
    {
        for (int i = 0; i < displayText.Length; i++)
        {
            textChars[i + textCharsStartIndex].GetComponent<WiggleLetter>().isWiggling = false;
            textChars[i + textCharsStartIndex].GetComponent<ShakeLetter>().isShaking = false;
            textChars[i + textCharsStartIndex].GetComponent<CharacterText>()?.ClearOffsets();
        }
    }

    private void PopulateModifierList(string text)
    {
        
        MatchCollection matches = Regex.Matches(text, tag_pattern);
        totalTextModifiers = new List<TextEffect>();

        // there has to be a better way, but right now my brain is tired
        int last_bold_index = -1;
        int last_italics_index = -1;
        int last_size_index = -1;
        int last_color_index = -1;
        int last_shake_index = -1;
        int last_wiggle_index = -1;
        int last_underline_index = -1;
        int last_subscript_index = -1;
        int last_superscript_index = -1;
        int substract_index = 0;

        if (matches.Count > 0)
        {
            foreach(Match match in matches)
            {
                // create text modifiers
                if (match.Value.Equals("<b>"))
                {
                    TextEffect bold = new TextEffect();
                    bold.type = TextEffect.TextEffectType.kBold;
                    bold.start_tag = "<b>";
                    bold.end_tag = "</b>";
                    bold.hasEndTag = true;
                    bold.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(bold);
                    last_bold_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("</b>"))
                {
                    if (last_bold_index >= 0)
                    {
                        totalTextModifiers[last_bold_index].end_index = match.Index - substract_index;
                        last_bold_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("<i>"))
                {
                    TextEffect italics = new TextEffect();
                    italics.type = TextEffect.TextEffectType.kItalics;
                    italics.start_tag = "<i>";
                    italics.end_tag = "</i>";
                    italics.hasEndTag = true;
                    italics.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(italics);
                    last_italics_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("</i>"))
                {
                    if (last_italics_index >= 0)
                    {
                        totalTextModifiers[last_italics_index].end_index = match.Index - substract_index;
                        last_italics_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("<u>"))
                {
                    TextEffect underline = new TextEffect();
                    underline.type = TextEffect.TextEffectType.kUnderline;
                    underline.start_tag = "<u>";
                    underline.end_tag = "</u>";
                    underline.hasEndTag = true;
                    underline.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(underline);
                    last_underline_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("</u>"))
                {
                    if (last_underline_index >= 0)
                    {
                        totalTextModifiers[last_underline_index].end_index = match.Index - substract_index;
                        last_underline_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("<sup>"))
                {
                    TextEffect superscript = new TextEffect();
                    superscript.type = TextEffect.TextEffectType.kSuperscript;
                    superscript.start_tag = "<sup>";
                    superscript.end_tag = "</sup>";
                    superscript.hasEndTag = true;
                    superscript.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(superscript);
                    last_superscript_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("</sup>"))
                {
                    if (last_superscript_index >= 0)
                    {
                        totalTextModifiers[last_superscript_index].end_index = match.Index - substract_index;
                        last_superscript_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("<sub>"))
                {
                    TextEffect subscript = new TextEffect();
                    subscript.type = TextEffect.TextEffectType.kSubscript;
                    subscript.start_tag = "<sub>";
                    subscript.end_tag = "</sub>";
                    subscript.hasEndTag = true;
                    subscript.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(subscript);
                    last_subscript_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("</sub>"))
                {
                    if (last_subscript_index >= 0)
                    {
                        totalTextModifiers[last_subscript_index].end_index = match.Index - substract_index;
                        last_subscript_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("<ln>"))
                {
                    TextEffect linebreak = new TextEffect();
                    linebreak.type = TextEffect.TextEffectType.kLineBreak;
                    linebreak.start_tag = "<ln>";
                    linebreak.hasEndTag = false;
                    linebreak.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(linebreak);
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<sprite="))
                {
                    TextEffect sprite = new TextEffect();
                    sprite.type = TextEffect.TextEffectType.kSprite;
                    sprite.start_tag = match.Value;
                    sprite.hasEndTag = false;
                    sprite.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(sprite);
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<size="))
                {
                    TextEffect size = new TextEffect();
                    size.type = TextEffect.TextEffectType.kSize;
                    size.start_tag = match.Value;
                    size.end_tag = "</size>";
                    size.hasEndTag = true;
                    size.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(size);
                    last_size_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("</size>"))
                {
                    if (last_size_index >= 0)
                    {
                        totalTextModifiers[last_size_index].end_index = match.Index - substract_index;
                        last_size_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<color="))
                {
                    TextEffect color = new TextEffect();
                    color.type = TextEffect.TextEffectType.kColor;
                    color.start_tag = match.Value;
                    color.end_tag = "</color>";
                    color.hasEndTag = true;
                    color.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(color);
                    last_color_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Equals("</color>"))
                {
                    if (last_color_index >= 0)
                    {
                        totalTextModifiers[last_color_index].end_index = match.Index - substract_index;
                        last_color_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<sp="))
                {
                    TextEffect speed = new TextEffect();
                    speed.type = TextEffect.TextEffectType.kSpeed;
                    speed.tag_data = match.Value.Substring("<sp=".Length, match.Value.Length - "<sp=".Length - 1);
                    speed.start_tag = match.Value;
                    speed.hasEndTag = false;
                    speed.isOneTimeEffect = false;
                    speed.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(speed);
                    last_color_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<shake="))
                {
                    TextEffect shake = new TextEffect();
                    shake.type = TextEffect.TextEffectType.kShake;
                    shake.tag_data = match.Value.Substring("<shake=".Length, match.Value.Length - "<shake=".Length - 1);
                    shake.start_tag = match.Value;
                    shake.hasEndTag = true;
                    shake.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(shake);
                    last_shake_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("</shake>"))
                {
                    if (last_shake_index >= 0)
                    {
                        totalTextModifiers[last_shake_index].end_index = match.Index - substract_index;
                        last_shake_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<wiggle="))
                {
                    TextEffect wiggle = new TextEffect();
                    wiggle.type = TextEffect.TextEffectType.kWiggle;
                    wiggle.tag_data = match.Value.Substring("<wiggle=".Length, match.Value.Length - "<wiggle=".Length - 1);
                    wiggle.start_tag = match.Value;
                    wiggle.hasEndTag = true;
                    wiggle.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(wiggle);
                    last_wiggle_index = totalTextModifiers.Count - 1;
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("</wiggle>"))
                {
                    if (last_wiggle_index >= 0)
                    {
                        totalTextModifiers[last_wiggle_index].end_index = match.Index - substract_index;
                        last_wiggle_index = -1;
                    }
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<p>"))
                {
                    TextEffect pause = new TextEffect();
                    pause.type = TextEffect.TextEffectType.kPause;
                    pause.start_tag = match.Value;
                    pause.hasEndTag = false;
                    pause.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(pause);
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<p="))
                {
                    TextEffect pause = new TextEffect();
                    pause.type = TextEffect.TextEffectType.kPause;
                    pause.tag_data = match.Value.Substring("<p=".Length, match.Value.Length - "<p=".Length - 1);
                    pause.start_tag = match.Value;
                    pause.hasEndTag = false;
                    pause.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(pause);
                    substract_index += match.Value.Length;
                }
                else if (match.Value.Contains("<wait>"))
                {
                    TextEffect wait = new TextEffect();
                    wait.type = TextEffect.TextEffectType.kWait;
                    wait.start_tag = match.Value;
                    wait.hasEndTag = false;
                    wait.start_index = match.Index - substract_index;
                    totalTextModifiers.Add(wait);
                    substract_index += match.Value.Length;
                }
            }
        }
    }

    private void ApplyTextModifiers(int index, GameObject textChar)
    {
        foreach (TextEffect mod in totalTextModifiers)
        {
            if (!mod.isUnityRichText())
            {
                if (index >= mod.start_index)
                {
                    if (mod.hasEndTag && index < mod.end_index)
                    {
                        DoModifierAction(mod, textChar);
                    }
                    else if(!mod.oneTimeEffectDone)
                    {
                        DoModifierAction(mod, textChar);
                        mod.oneTimeEffectDone = true;
                    }
                    else if (!mod.hasEndTag && !mod.isOneTimeEffect)
                    {
                        DoModifierAction(mod, textChar);
                    }
                }
            }
        }
    }

    private void DoModifierAction(TextEffect mod, GameObject textChar)
    {
        switch (mod.type)
        {
            case TextEffect.TextEffectType.kLineBreak:
                {
                    NewLine();
                    textChar.GetComponent<RectTransform>().position = startPos;
                    break;
                }
            case TextEffect.TextEffectType.kSpeed:
                {
                    current_char_delay = float.Parse(mod.tag_data);
                    break;
                }
            case TextEffect.TextEffectType.kShake:
                {
                    Vector3 charPos = startPos + new Vector3(currentLineOffset, 0, 0);
                    ShakeLetter shake = textChar.GetComponent<ShakeLetter>();
                    string[] tags = mod.tag_data.Split(",".ToCharArray());
                    if (tags.Length > 1)
                    {
                        shake.shake_radius = float.Parse(tags[0]);
                        shake.shake_speed = float.Parse(tags[1]);
                    }

                    if (isActiveDialogue)
                        shake.StartShake(startPos, currentLineOffset);
                    else
                        shake.StartShake(charPos);

                    break;
                }
            case TextEffect.TextEffectType.kWiggle:
                {
                    WiggleLetter wiggle = textChar.GetComponent<WiggleLetter>();
                    CharacterText carTex = textChar.GetComponent<CharacterText>();
                    string[] tags = mod.tag_data.Split(",".ToCharArray());
                    if (tags.Length > 1)
                    {
                        wiggle.wiggle_height = float.Parse(tags[0]);
                        wiggle.wiggle_speed = float.Parse(tags[1]);
                    }
                    wiggle.StartWiggle(isActiveDialogue);
                    break;
                }
            case TextEffect.TextEffectType.kPause:
                {
                    isPaused = true;
                    if (mod.tag_data != null)
                    {
                        pause_time = float.Parse(mod.tag_data);
                    }
                    else
                    {
                        pause_time = default_pause_time;
                    }
                    break;
                }
            case TextEffect.TextEffectType.kWait:
                {
                    //isWaiting = true;
                    break;
                }
        }
    }

    private string GetStartCharTags(int index)
    {
        if (totalTextModifiers.Count > 0)
        {
            string tags = "";
            foreach (TextEffect effect in totalTextModifiers)
            {
                if (effect.isUnityRichText())
                {
                    if (effect.isEffectActive(index))
                    {
                        tags += effect.start_tag;
                    }
                    else if (index >= effect.start_index && !effect.oneTimeEffectDone)
                    {
                        tags += effect.start_tag;
                        effect.oneTimeEffectDone = true;
                    }
                    else if (index >= effect.start_index && !effect.hasEndTag && !effect.isOneTimeEffect)
                    {
                        tags += effect.start_tag;
                    }
                }
            }
            return tags;
        }
        else
        {
            return "";
        }
    }

    private string GetEndCharTags(int index)
    {
        if (totalTextModifiers.Count > 0)
        {
            string tags = "";
            for (int i = totalTextModifiers.Count - 1; i >=0; --i)
            {
                TextEffect effect = totalTextModifiers[i];
                if (effect.isUnityRichText())
                {
                    if (effect.isEffectActive(index))
                    {
                        tags += effect.end_tag;
                    }
                }
            }
            return tags;
        }
        else
        {
            return "";
        }
    }

    public bool iSDone()
    {
        return done;
    }

    private float GetCharacterWidth(TextMeshProUGUI textComponent)
    {
        string displayText = Regex.Replace(textComponent.text, tag_pattern, "");
        if (displayText == " ")
            return textComponent.fontSize / 4.0f;

        return textComponent.preferredWidth;
    }
}
