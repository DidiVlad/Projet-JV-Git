using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PaintHandler : MonoBehaviour
{
    public Color CurrentColor;
    private int CurrentColorNumber = 0;
    private List<string> unlockedColors = new List<string>(); 
    public GameObject Palette;
    private Dictionary<string, Color> ColorValues = new Dictionary<string, Color>()
    {
        {"blue", Color.blue},
        {"red", Color.red},
        {"black", Color.black},
        {"green", Color.green},
        {"yellow", Color.yellow}
    };

    private Dictionary<string, bool> Colors = new Dictionary<string, bool>()
    {
        {"blue", false}, 
        {"red", false},
        {"black", true},
        {"green", false},
        {"yellow", false}
    };

    void Start()
    {
        unlockedColors.Add("black");
        CurrentColor = ColorValues["black"];
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ChangeColor(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ChangeColor(-1);
        }
    }

void ChangeColor(int direction)
{
    if (unlockedColors.Count == 0) return;

    CurrentColorNumber += direction;

    if (CurrentColorNumber >= unlockedColors.Count) CurrentColorNumber = 0;
    if (CurrentColorNumber < 0) CurrentColorNumber = unlockedColors.Count - 1;

    CurrentColor = ColorValues[unlockedColors[CurrentColorNumber]];

    Transform selectedColorTransform = Palette.transform.Find(unlockedColors[CurrentColorNumber]);
    
    if (selectedColorTransform != null)
    {
        Transform outline = Palette.transform.Find("Outline");
        if (outline != null)
        {
            outline.position = selectedColorTransform.position;
        }

    }
}

public void UnlockColor(string colorToUnlock)
{
    if (Colors.ContainsKey(colorToUnlock) && !Colors[colorToUnlock])
    {
        Colors[colorToUnlock] = true;
        unlockedColors.Add(colorToUnlock);
        Transform newCTransform = Palette.transform.Find(colorToUnlock);
        if (newCTransform != null)
        {
            UnityEngine.UI.Image newC = newCTransform.GetComponent<UnityEngine.UI.Image>();
            if (newC != null) 
            {
                Color c = newC.color;
                c.a = 1f;
                newC.color = c; 
            }
        }
    }
}

}