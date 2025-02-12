using System;
using UnityEngine;

public class Camera_Follow : MonoBehaviour{
public Transform player;
    void Update()
    
    {
    transform.position = new Vector3(player.transform.position.x , player.transform.position.y + 1, -10);
    }
}
