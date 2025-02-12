using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PaintHandler : MonoBehaviour
{
    public Color CurrentColor;
    private int CurrentColorNumber = 0;
    private List<string> unlockedColors = new List<string>(); 
    public GameObject Palette;
    private Dictionary<string, Color> ColorValues = new Dictionary<string, Color>()
    {
        {"black", Color.black},
        {"yellow", Color.yellow},
        {"green", Color.green},
        {"red", Color.red},
        {"blue", Color.blue}
    };

    private Dictionary<string, bool> Colors = new Dictionary<string, bool>()
    {
        {"black", true}, 
        {"yellow", false},
        {"green", false},
        {"red", false},
        {"blue", false}
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
        Debug.Log("Current Color: " + unlockedColors[CurrentColorNumber]);
    }

    public void UnlockColor(string colorToUnlock)
    {
        print("triggered");
        if (Colors.ContainsKey(colorToUnlock) && !Colors[colorToUnlock])
        {
            print("good");
            Colors[colorToUnlock] = true;
            unlockedColors.Add(colorToUnlock);
            Debug.Log(colorToUnlock + " unlocked!");
            Transform newC = Palette.transform.Find(colorToUnlock);
            SpriteRenderer colorC = newC.GetComponent<SpriteRenderer>();
            Color c = colorC.color;
            c.a = 1f;
        }
    }
}
