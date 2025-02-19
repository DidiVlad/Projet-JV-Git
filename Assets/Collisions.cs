using UnityEngine;

public class Collisions : MonoBehaviour
{
    public GameObject FlakePrefab;
    public GameObject proj;

    private void LeaveFlake(Vector2 position, Quaternion rotation)
    {
        if (FlakePrefab == null) return;

        GameObject flake = Instantiate(FlakePrefab, position, rotation);
        SpriteRenderer flakeRenderer = flake.GetComponent<SpriteRenderer>();
        flakeRenderer.color = proj.GetComponent<SpriteRenderer>().color;

        Destroy(flake, 3);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            LeaveFlake(collision.ClosestPoint(transform.position), collision.transform.rotation);
        }
        Destroy(gameObject);
    }
}
