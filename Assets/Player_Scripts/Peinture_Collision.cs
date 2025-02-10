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
    public ParticleSystem FireEffect;
    SpriteRenderer sprt_renderer;
    private bool hasHitEnemy = false;
    private GameObject Player;

    void Start()
    {
        Player =  GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<SpriteRenderer>().color = Player.GetComponent<PaintHandler>().CurrentColor;
    }

  private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Ground"))
    {
        Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, other.transform.up);

        LeaveFlake(other.ClosestPoint(transform.position), surfaceRotation );
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
    }
    if (hasHitEnemy) return;
    else if (other.gameObject.CompareTag("Enemy"))
    {
        hasHitEnemy = true;
        HitEffect(other.gameObject);
        PaintEffect(Peinture.GetComponent<SpriteRenderer>().color, other.gameObject);
    }
    else if (other.gameObject.CompareTag("Void"))
    {
        Destroy(gameObject);
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
        Destroy(gameObject);
    }
    else if (color == Color.red)
    {
        StartCoroutine(RedEffect(Entity));
    }
    else if (color == Color.blue)
    {
        StartCoroutine(BlueEffect(Entity));
    }  
}

    private IEnumerator BlueEffect(GameObject Entity)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        Entity.GetComponent<HealthHandler>().HP -= 0.5f;
        Entity.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Entity.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(1f);
        Entity.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Entity.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(gameObject);
    }

private IEnumerator RedEffect(GameObject Entity)
{
    gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
    
    for (int i = 0; i < 2; i++)
    {
        if (Entity != null)
        {
            Entity.GetComponent<SpriteRenderer>().color = Color.red;

            ParticleSystem newEffect = Instantiate(FireEffect, Entity.transform.position, Quaternion.identity);
            newEffect.transform.parent = Entity.transform; 
            newEffect.Play();
            Entity.GetComponent<HealthHandler>().HP -= 0.5f;

            yield return new WaitForSeconds(newEffect.main.duration);
            if (Entity != null)
            {
                Entity.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(newEffect.gameObject);
            }
        }
    }
}



}



