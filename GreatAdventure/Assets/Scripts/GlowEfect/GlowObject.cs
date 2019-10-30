﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowObject : MonoBehaviour
{
    public Color GlowColor;
    public float LerpFactor = 10;

    private List<Material> materials = new List<Material>();
    private Color currentColor;
    private Color targetColor;

    private void Awake()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            materials.AddRange(renderer.materials);
        }
    }

    public void Start()
    {
        TurnOnGlow();
    }

    public void TurnOnGlow()
    {
        targetColor = GlowColor;
        enabled = true;
    }

    public void TurnOffGlow()
    {
        targetColor = Color.black;
        enabled = false;
    }

    void Update()
    {
        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * LerpFactor);

        for (int i = 0; i < materials.Count; ++i)
        {
            materials[i].SetColor("_GlowColor", currentColor);
        }

        if (currentColor.Equals(targetColor))
        {
            enabled = false;
        }
    }
}
