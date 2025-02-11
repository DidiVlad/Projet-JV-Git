using UnityEngine;

public class ColorsUnlocker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("unlcoked");
            collision.gameObject.GetComponent<PaintHandler>().UnlockColor(gameObject.name);
            Destroy(gameObject);
        }
    }
}
