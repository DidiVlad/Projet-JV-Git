using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    private Dictionary<Color, bool> ColorValues = new Dictionary<Color, bool>()
    {
        {Color.blue, false},
        {Color.red, false},
        {Color.black, false},
        {Color.green, false},
        {Color.yellow, false}
    };

    void Update()
    {
        if (AllColors())
        {
            UnlockDoor();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Peinture" && ColorValues[collision.gameObject.GetComponent<SpriteRenderer>().color] == false)
        {
            print("triggered");
            SetColorTrue(collision.gameObject.GetComponent<SpriteRenderer>().color);
            Destroy(collision.gameObject);
        }
    }

    void SetColorTrue(Color color)
    {
        ColorValues[color] = true;
    }

    private bool AllColors()
    {
        foreach (bool i in ColorValues.Values)
        {
            if(i == false)
        {
            return false;
        }
        }
        return true;
    } 

    private void UnlockDoor()
    {
            Destroy(gameObject);
    }
}
