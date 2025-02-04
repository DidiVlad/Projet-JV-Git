using UnityEngine;

public class Water_Damage : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        other.gameObject.GetComponent<PlayerHealthMain>().HP = 0;
    }
}
}
