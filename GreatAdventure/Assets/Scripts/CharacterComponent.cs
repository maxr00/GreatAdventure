using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    public enum Emotion
    {
        Happy,
        ANGRYASFUCK,
    };

    public Sprite characterIcon;
    public Vector3 characterOffset;
    public float cameraRadius; // Value used to describe radius of cicle that the camera should keep in frame during dialogue

    private Emotion currentEmotion;  
    private Vector3 currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentOffset = transform.position + characterOffset;
    }

    public Vector3 GetCurrentOffset()
    {
        return currentOffset;
    }

    public void OnCharacterTalk(string text)
    {
        GetComponent<CharacterVoice>()?.Speak(text);
    }

}
