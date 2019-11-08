using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterVoice : MonoBehaviour
{
    const int twoSyllableChars = 5; // Minimum num of characters a word contains to be 2 syllables
    const int threeSyllableChars = 7; // Minimum num of characters a word contains to be 3 syllables

    bool stopped = false;

    AudioSource src;

    public List<AudioClip> clips = new List<AudioClip>(6);

    public float charPauseTime = 0.0f;
    public float wordPauseTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void StopSpeaking()
    {
        stopped = true;
    }

    public void Speak(string text)
    {
        stopped = false;

        string[] words = text.Split(new char[]{' ','-'});

        StartCoroutine(SpeakSentence(words));
    }

    private IEnumerator SpeakSentence(string[] words)
    {
        foreach (string word in words)
        {
            if (stopped)
                break;

            yield return StartCoroutine(SayWord(word));
        }
    }

    private IEnumerator SayWord(string word)
    {
        var sounds = ChooseSounds(word);

        foreach(AudioClip clip in sounds)
        {
            if (stopped)
                break;

            if(clip != null)
            {
                src.clip = clip;
                src.Play();
                yield return new WaitForSeconds(clip.length + charPauseTime);
            }
        }
        yield return new WaitForSeconds(wordPauseTime);
    }

    private List<AudioClip> ChooseSounds(string word)
    {
        List<AudioClip> sounds = new List<AudioClip>();

        word = RemoveDups(word); // avoid screaming multiple syllables ("aaaaaaaaa" should only be 1 syllable, not 4)

        int syllables = 1;
        if (word.Length >= twoSyllableChars)   syllables++;
        if (word.Length >= threeSyllableChars) syllables++;

        int partLen = word.Length / syllables;
        for (int p = 0; p < syllables; p++)
        {
            string part = word.Substring(p * partLen, partLen);

            int index = Mathf.Abs(part.GetHashCode()) % clips.Count; // Choose a unique sound for this part. Repeated words are guarunteed to be pronounced the same.

            sounds.Add(clips[index]);
        }

        return sounds;
    }

    string RemoveDups(string word)
    {
        char[] c = word.ToCharArray();

        if (c.Length == 0)
            return word;

        string text = "" + c[0];
        for (int i = 1; i < c.Length - 1; i++)
        {
            if (c[i] != c[i + 1])
                text += c[i];
        }
        return text;
    }
}
