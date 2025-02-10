using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public float HP = 3f; 
    public float MaxHP = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
