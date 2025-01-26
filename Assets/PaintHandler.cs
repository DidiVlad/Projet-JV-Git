using System.Collections.Generic;
using Mono.Cecil.Cil;
using NUnit.Framework;
using UnityEngine;

public class PaintHandler : MonoBehaviour
{
   
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
        print(Colors["blue"]);
    }


    void Update()
    {
        
    }

    void UnlockColor(string ColorToUnlock)
    {
        Colors[ColorToUnlock] = true;
    }
}
