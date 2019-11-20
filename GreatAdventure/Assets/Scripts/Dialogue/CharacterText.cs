using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterText : MonoBehaviour
{
    public float anchorMin, anchorMax;
    public float left;
    public float top;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
    }

    public void ClearOffsets()
    {
        GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        anchorMin = 0; anchorMax = 0; left = 0; top = 0;
        UpdateNow();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().offsetMin = new Vector2(left, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, top);

        GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
        GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, anchorMax);
    }

    public void UpdateNow()
    {
        GetComponent<RectTransform>().offsetMin = new Vector2(left, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, top);

        GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
        GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, anchorMax);
    }
}
