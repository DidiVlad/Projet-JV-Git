using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;

public class PaintHandler : MonoBehaviour
{
    public Color CurrentColor;
    private int maxColors = 0;
    private Dictionary<string, bool> Colors = new Dictionary<string, bool>()
    {
        {"black", true},
        {"yellow", false},
        {"green", false},
        {"red", false},
        {"blue", false}

    };

    void Update()
    {
        //scroll wheel -> change current color
    }


    void UnlockColor(string ColorToUnlock)
    {
        Colors[ColorToUnlock] = true;
        maxColors += 1;
        for (int i = 0; i < maxColors; i++)
        {
            //update ui
        }
    }
}
