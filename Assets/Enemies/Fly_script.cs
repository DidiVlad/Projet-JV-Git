using UnityEngine;

public class Fly_script : MonoBehaviour
{
    public GameObject plr; 
    public float DetectRadius = 7f; 
    public float EnemySpeed = 5f; 

    private Rigidbody2D rb;

    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
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
 
    }

    bool CheckForDetection()
    {
        float distanceToPlayer = Vector2.Distance(plr.transform.position, transform.position);
        return distanceToPlayer <= DetectRadius;
    }

    void Chase()
    {
        Vector2 direction = (plr.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * EnemySpeed, direction.y * EnemySpeed);
    }
}
