using System;
using System.Collections;
using Mono.Cecil.Cil;
using NUnit.Framework.Constraints;
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
            HitEffect(collision.gameObject);
            PaintEffect(Peinture.GetComponent<SpriteRenderer>().color, collision.gameObject);

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
void HitEffect(GameObject Entity)
{
    if (PaintHitEffect != null)
    {
        Color paint_color = Peinture.GetComponent<SpriteRenderer>().color;

        ParticleSystem newEffect = Instantiate(PaintHitEffect, Entity.transform.position, Entity.transform.rotation);

        newEffect.transform.parent = Entity.transform;
        var mainModule = newEffect.main; 
        mainModule.startColor = paint_color;

        if (newEffect.trails.enabled)
        {
            var trailsModule = newEffect.trails; 
            trailsModule.colorOverTrail = new ParticleSystem.MinMaxGradient(paint_color);
        }
        newEffect.Emit(3);
        Destroy(newEffect.gameObject, 0.5f);
    }
    else
    {
        Debug.LogWarning("PaintHitEffect is not assigned!");
    }
}

void PaintEffect(Color color, GameObject Entity)
{
    if (color == Color.black)
    {
        Entity.GetComponent<HealthHandler>().HP -= 1;
    }
    else if (color == Color.red)
    {
        StartCoroutine(RedPaintEffect(Entity, 0.5f, 2, 0.5f));
    }
    Destroy(gameObject);
}

IEnumerator RedPaintEffect(GameObject entity, float damagePerTick, int tickCount, float delayBetweenTicks)
{
    for (int i = 0; i < tickCount; i++)
    {
        print("looping");
        // Apply damage
        entity.GetComponent<HealthHandler>().HP -= damagePerTick;
        Debug.Log( entity.GetComponent<HealthHandler>().HP);

        // Wait for the specified delay
        yield return new WaitForSeconds(delayBetweenTicks);
    }
}


}



