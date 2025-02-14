using UnityEngine;

public class ChackpointCheck : MonoBehaviour
{
    public GameObject player;
    private float Detectradius = 1f;
    public GameObject CPText;
    public ParticleSystem CPEffect;

    void Update()
    {
        Transform nearestCP = ReturnNearestCP();

        if (nearestCP != null)
        {
            // Show text above checkpoint
            CPText.transform.position = nearestCP.position + new Vector3(0, 0.5f, 0);
            CPText.SetActive(true);

            // When player presses E
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.GetComponent<PlayerHealthMain>().Lastcheckpoint = nearestCP;

                // Spawn particle effect at checkpoint
                ParticleSystem newCPEffect = Instantiate(CPEffect, nearestCP.position, Quaternion.identity);

                newCPEffect.Emit(20);
                Destroy(newCPEffect, 1f);
            }
        }
        else
        {
            CPText.SetActive(false);
        }
    }

    Transform ReturnNearestCP()
    {
        Transform nearestCP = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform CP in gameObject.transform)
        {
            float dist = Vector2.Distance(CP.position, player.transform.position);
            if (dist < Detectradius && dist < closestDistance)
            {
                closestDistance = dist;
                nearestCP = CP;
            }
        }
        return nearestCP;
    }
}
