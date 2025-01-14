using UnityEngine;

public class IA_Main : MonoBehaviour
{
    public GameObject plr; 
    public float DetectRadius = 5f; 
    public float EnemySpeed = 2f; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (CheckForDetection())
        {
            Chase();
        }
        else
        {
            rb.linearVelocity = Vector2.zero; 
        }
    }

    bool CheckForDetection()
    {
        float distanceToPlayer = Vector2.Distance(plr.transform.position, transform.position);
        return distanceToPlayer <= DetectRadius;
    }

    void Chase()
    {
        Vector2 direction = (plr.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * EnemySpeed, rb.linearVelocity.y);
    }
}
