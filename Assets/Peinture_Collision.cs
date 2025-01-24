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
    private bool hasHitEnemy = false;

  private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Ground"))
    {
        Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, other.transform.up);
        print(other.transform.up);

        LeaveFlake(other.ClosestPoint(transform.position), surfaceRotation );
        Destroy(gameObject);
    }
    if (hasHitEnemy) return;
    else if (other.gameObject.CompareTag("Enemy"))
    {
        hasHitEnemy = true;
        print("hit");
        HitEffect(other.gameObject);
        PaintEffect(Peinture.GetComponent<SpriteRenderer>().color, other.gameObject);
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
    Destroy(gameObject);
}



}



