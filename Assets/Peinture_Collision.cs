using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;

public class PeintureCollision : MonoBehaviour
{
    public GameObject FlakePrefab;
    public float FlakeLifetime = 3f;
    public GameObject Peinture;
    public ParticleSystem PaintHitEffect;
    SpriteRenderer sprt_renderer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, collision.GetContact(0).normal);

            LeaveFlake(collision.GetContact(0).point, surfaceRotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit: " + collision.gameObject.name);
            collision.gameObject.GetComponent<HealthHandler>().HP -= 1;
            Destroy(gameObject);
        }
        else
        {
        }
    }

    private void LeaveFlake(Vector2 position, Quaternion rotation)
    {
        if (FlakePrefab != null)
        {
            GameObject flake = Instantiate(FlakePrefab, position, rotation);
            sprt_renderer = flake.GetComponent<SpriteRenderer>();
            sprt_renderer.color = Peinture.GetComponent<SpriteRenderer>().color;

            Destroy(flake, FlakeLifetime);
        }
    }

  //  void HitEffect(GameObject Entity)
   // {
   //     PaintHitEffect.Emit(Entity.transform.position)
   // }
}
