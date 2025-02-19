using System.Collections;
using UnityEngine;

public class Fly_script : MonoBehaviour
{
    public GameObject plr; 
    public float DetectRadius = 7f; 
    public float EnemySpeed = 5f;
    public int ProjectileSpeed = 15;
    public float ProjectileCD = 1f;
    public GameObject Projectile; 

    private Rigidbody2D rb;
    private bool canAttack = true;

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
                
                if (canAttack)
                {
                    StartCoroutine(Attack());
                }
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
        Vector2 direction = (plr.transform.position + new Vector3(0,2,0) - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * EnemySpeed, direction.y * EnemySpeed);
    }

    IEnumerator Attack()
    {
        canAttack = false;

        GameObject newProjectile = Instantiate(Projectile);
        newProjectile.transform.position = transform.position;
        Vector2 direction = (plr.transform.position - transform.position).normalized;
        newProjectile.GetComponent<Rigidbody2D>().AddForce(direction*5, ForceMode2D.Impulse);

        yield return new WaitForSeconds(ProjectileCD);
        canAttack = true;
    }
}
