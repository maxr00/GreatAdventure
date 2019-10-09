using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect
{
    public enum TextEffectType:int
    {
        kItalics = 0,
        kBold,
        kLineBreak,
        kColor,
        kSize,
        kUnderline,
        kSubscript,
        kSuperscript,
        kSprite,
        kSpeed,
        kShake,
        kWiggle,
        kPause,
        kWait,

        kEffectCount
    }

    public TextEffectType type;
    public string start_tag;
    public string end_tag;
    public string tag_data;
    public bool hasEndTag;
    public bool isOneTimeEffect = true;
    public bool oneTimeEffectDone = false;
    public int start_index;
    public int end_index;

    public bool isUnityRichText()
    {
        bool isRichText =    (type == TextEffectType.kItalics)
                          || (type == TextEffectType.kBold)
                          || (type == TextEffectType.kSize)
                          || (type == TextEffectType.kUnderline)
                          || (type == TextEffectType.kSubscript)
                          || (type == TextEffectType.kSuperscript)
                          || (type == TextEffectType.kSprite)
                          || (type == TextEffectType.kColor);
        return isRichText;
    }

    public bool isEffectActive(int index)
    {
        return (index >= start_index && index < end_index);
    }
}
