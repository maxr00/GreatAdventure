using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    public enum Emotion
    {
        Unchanged,
        Happy,
        HappyTalk,
        Sad,
        SadTalk,
    };

    [System.Serializable]
    public class EmotionAnimation
    {
        public Emotion emotion;
        public string animName;
    };

    public Sprite characterIcon;
    public Vector3 characterOffset;
    public Vector3 characterBubbleOffset;
    public float cameraRadius; // Value used to describe radius of cicle that the camera should keep in frame during dialogue

    public Animator emotionAnim;
    public List<EmotionAnimation> emotionAnimations;

    private Emotion currentEmotion;  
    private Vector3 currentOffset;
    private Vector3 currentBubblePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentOffset = transform.position + characterOffset;
        currentBubblePos = transform.position + characterBubbleOffset;
    }

    public Vector3 GetCurrentOffset()
    {
        return currentOffset;
    }
    public Vector3 GetCurrentBubblePos()
    {
        return currentBubblePos;
    }

    public void OnCharacterTalk(string text)
    {
        GetComponent<CharacterVoice>()?.Speak(text);
    }

    public void SetCharacterEmotion(Emotion emotion)
    {
        if (emotion == Emotion.Unchanged)
            return;

        currentEmotion = emotion;

        Debug.Log(name + " set to" + currentEmotion);

        if(emotionAnim)
        {
            foreach(var anim in emotionAnimations)
            {
                if(emotion == anim.emotion)
                {
                    emotionAnim.Play(anim.animName);
                }
            }
        }
    }

    public void testDialogueEvent()
    {
        Debug.Log("dialogue event called");
        transform.localScale = new Vector3(10, 1, 1);
    }

    public void testDialogueEvent2()
    {
        Debug.Log("dialogue event 2 called");
        transform.localScale = new Vector3(1, 10, 1);
    }
}
