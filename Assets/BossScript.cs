using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private GameObject plr;
    
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private float SinAmplitude = 2f;
    [SerializeField] private float SinFrequency = 2f; 
    [SerializeField] private float HorizontalRange = 5f;
    [SerializeField] private float DetectRadius = 10f; 

    [SerializeField] private GameObject Pinceau;
    [SerializeField] private float projV = 500f;
    public GameObject proj;
    public Transform ShootPoint;

    private bool IsShooting = false;
    private float startY;
    private float offsetX;

    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        startY = transform.position.y;
        offsetX = Random.Range(0f, Mathf.PI * 2f); 
    }

    void Update()
    {
        if (plr == null) return;

        if (Vector2.Distance(plr.transform.position, transform.position) <= DetectRadius)
        {
            LoopAbovePlayer();

            orientP();

            // Shooting logic
            if (!IsShooting)
            {
                IsShooting = true;
                StartCoroutine(Shoot(Random.Range(1, 4)));
            }
        }
    }

private void LoopAbovePlayer()
{
    float playerX = plr.transform.position.x;
    float playerY = plr.transform.position.y;

    // Calculate new X position for the infinity loop (figure 8)
    float targetX = playerX + Mathf.Sin(Time.time * MoveSpeed) * HorizontalRange;

    // Calculate new Y position, ensuring it stays above the player
    float minY = playerY + 3f; // Adjust this value to control how high the boss stays
    float newY = minY + Mathf.Sin(Time.time * SinFrequency + offsetX) * SinAmplitude;

    // Apply the new position to the boss
    transform.position = new Vector3(targetX, newY, transform.position.z);
}


    private void orientP()
    {
        Vector2 plrPos = plr.transform.position;
        Vector2 direction = plrPos - (Vector2)Pinceau.transform.position;
        Pinceau.transform.right = direction;
    }

    private IEnumerator Shoot(int nb)
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        for (int i = 0; i < nb; i++)
        {
            GameObject newproj = Instantiate(proj, ShootPoint.position, ShootPoint.rotation);
            newproj.GetComponent<Rigidbody2D>().AddForce(newproj.transform.right * projV);
            yield return new WaitForSeconds(0.5f);
        }
        IsShooting = false;
    }
}
