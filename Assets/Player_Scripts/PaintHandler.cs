using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PaintHandler : MonoBehaviour
{
    public Color CurrentColor;
    private int CurrentColorNumber = 0;
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && CurrentColorNumber < Colors.Count) 
        {
            CurrentColorNumber += 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && CurrentColorNumber > 0) 
        {
            CurrentColorNumber -= 1;
        }
    }


    void UnlockColor(string ColorToUnlock)
    {
        Colors[ColorToUnlock] = true;
        maxColors += 1;
        for (int i = 0; i < maxColors; i++)
        {
            //update ui
            print("unlocked color");
        }
    }
}
