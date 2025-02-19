using UnityEngine;

public class DamageScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
