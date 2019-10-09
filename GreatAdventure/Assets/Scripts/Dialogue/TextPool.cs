using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPool : MonoBehaviour
{
    public GameObject textPrefab;

    List<GameObject> availableTextChar = new List<GameObject>();

    public List<GameObject> Claim(int num)
    {
        if (num > availableTextChar.Count)
        {
            for(int i = 0; i < num; i++)
            {
                availableTextChar.Add(GameObject.Instantiate(textPrefab, transform));
            }
        }
        var list = availableTextChar.GetRange(0, num);
        availableTextChar.RemoveRange(0, num);
        return list;
    }

    public void Unclaim(List<GameObject> text)
    {
        foreach (GameObject obj in text)
        {
            obj.GetComponent<TextMeshProUGUI>().text = "";
        }
        availableTextChar.AddRange(text);
    }
}
