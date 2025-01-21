using Unity.Mathematics;
using UnityEngine;

public class Fly_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.01f * math.log10(Time.time), gameObject.transform.position.y + 0.01f * Mathf.Sin(Time.time));
    }
}
