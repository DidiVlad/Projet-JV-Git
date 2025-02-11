using UnityEngine;

public class deletealll : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
