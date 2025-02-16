using UnityEngine;

public class DamageScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("touch player");
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealthMain>().HP -= 1; 
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
