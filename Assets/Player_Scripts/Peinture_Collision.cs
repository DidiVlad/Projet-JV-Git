using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PeintureCollision : MonoBehaviour
{
    [Header("Paint Settings")]
    public GameObject FlakePrefab;
    public float FlakeLifetime = 3f;
    public GameObject Peinture;
    public ParticleSystem PaintHitEffect;
    public ParticleSystem FireEffect;

    [Header("Torch Settings")]
    public Sprite torch_ignite;

    private SpriteRenderer spriteRenderer;
    private bool hasHitEnemy = false;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Player.GetComponent<PaintHandler>().CurrentColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                LeaveFlake(other.ClosestPoint(transform.position), other.transform.rotation);
                spriteRenderer.color = Color.clear;
                break;

            case "Enemy":
                if (!hasHitEnemy)
                {
                    hasHitEnemy = true;
                    HitEffect(other.gameObject);
                    PaintEffect(Peinture.GetComponent<SpriteRenderer>().color, other.gameObject);
                }
                break;

            case "Void":
                Destroy(gameObject);
                break;

            case "EnvItem":
                EnvEffect(Peinture.GetComponent<SpriteRenderer>().color, other.gameObject);
                break;
        }
    }

    private void LeaveFlake(Vector2 position, Quaternion rotation)
    {
        if (FlakePrefab == null) return;

        GameObject flake = Instantiate(FlakePrefab, position, rotation);
        SpriteRenderer flakeRenderer = flake.GetComponent<SpriteRenderer>();
        flakeRenderer.color = Peinture.GetComponent<SpriteRenderer>().color;

        if (flakeRenderer.color == Color.yellow)
        {
            flake.GetComponent<Light2D>().enabled = true;
        }

        Destroy(flake, FlakeLifetime);
    }

    private void HitEffect(GameObject entity)
    {
        if (PaintHitEffect == null)
        {
            Debug.LogWarning("PaintHitEffect is not assigned!");
            return;
        }

        Color paintColor = Peinture.GetComponent<SpriteRenderer>().color;
        ParticleSystem newEffect = Instantiate(PaintHitEffect, entity.transform.position, entity.transform.rotation);
        newEffect.transform.parent = entity.transform;

        var mainModule = newEffect.main;
        mainModule.startColor = paintColor;

        if (newEffect.trails.enabled)
        {
            var trailsModule = newEffect.trails;
            trailsModule.colorOverTrail = new ParticleSystem.MinMaxGradient(paintColor);
        }

        newEffect.Emit(3);
        Destroy(newEffect.gameObject, 0.5f);
    }

private void PaintEffect(Color color, GameObject entity)
{
    if (color.Equals(Color.black)) 
    {
        entity.GetComponent<HealthHandler>().HP -= 1;
        Destroy(gameObject);
    }
    else if (color.Equals(Color.red))
    {
        StartCoroutine(RedEffect(entity));
    }
    else if (color.Equals(Color.blue))
    {
        StartCoroutine(BlueEffect(entity));
    }
    else if (color.Equals(Color.green))
    {
        StartCoroutine(GreenEffect(entity));
    }
    else if (color.Equals(Color.yellow))
    {
        YellowEffect(entity);
    }
}


    private IEnumerator BlueEffect(GameObject entity)
    {
        if (entity == null) yield break;

        spriteRenderer.color = Color.clear;
        entity.GetComponent<HealthHandler>().HP -= 0.5f;

        Rigidbody2D rb = entity.GetComponent<Rigidbody2D>();
        SpriteRenderer entityRenderer = entity.GetComponent<SpriteRenderer>();

        if (rb)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        entityRenderer.color = Color.blue;
        yield return new WaitForSeconds(1f);

        if (entity)
        {
            if (rb) rb.bodyType = RigidbodyType2D.Dynamic;
            entityRenderer.color = Color.white;
            Destroy(gameObject);
        }
    }

    private IEnumerator RedEffect(GameObject entity)
    {
        if (entity == null) yield break;

        spriteRenderer.color = Color.clear;

        for (int i = 0; i < 3; i++)
        {
            if (entity == null) yield break;

            entity.GetComponent<SpriteRenderer>().color = Color.red;

            ParticleSystem newEffect = Instantiate(FireEffect, entity.transform.position, Quaternion.identity);
            newEffect.transform.parent = entity.transform;
            newEffect.Play();

            entity.GetComponent<HealthHandler>().HP -= 0.5f;
            yield return new WaitForSeconds(newEffect.main.duration);

            if (entity)
            {
                entity.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(newEffect.gameObject);
            }
        }
    }

    private IEnumerator GreenEffect(GameObject entity)
    {
        if (entity == null) yield break;

        spriteRenderer.color = Color.clear;
        entity.GetComponent<HealthHandler>().HP -= 0.75f;

        Rigidbody2D rb = entity.GetComponent<Rigidbody2D>();
        SpriteRenderer entityRenderer = entity.GetComponent<SpriteRenderer>();

        if (rb)
        {
            Vector2 pushDirection = (entity.transform.position - transform.position).normalized;
            rb.AddForce(pushDirection * 50f, ForceMode2D.Impulse);
        }

        entityRenderer.color = Color.green;
        yield return new WaitForSeconds(1f);

        if (entity)
        {
            entityRenderer.color = Color.white;
        }
    }

    private void YellowEffect(GameObject Entity)
    {
        Destroy(gameObject);
    }

    private void EnvEffect(Color color, GameObject obj)
    {
        switch (obj.name)
        {
            case "Lianes" when color == Color.red:
                FireOnEnv(obj);
                break;

            case "Light" when color == Color.yellow:
                Destroy(gameObject);
                Light2D light = obj.GetComponent<Light2D>();
                SpriteRenderer skin = obj.GetComponent<SpriteRenderer>();

                skin.sprite = torch_ignite;
                light.enabled = true;
                break;
            case "Seed" when color == Color.green:
                GreenOnV(obj);
                break;

        }
    }

    private void FireOnEnv(GameObject obj)
    {
        Destroy(gameObject);
        ParticleSystem newEffect = Instantiate(FireEffect, obj.transform.position, Quaternion.identity);
        newEffect.transform.parent = obj.transform;
        newEffect.Play();

        Destroy(obj, 1.5f);
        Destroy(newEffect, 1.5f);
    }

    private void GreenOnV(GameObject obj)
    {
        print("triggered");
        StartCoroutine(GrowthSequence(obj));
    }

    private IEnumerator GrowthSequence(GameObject obj)
    {
        yield return StartCoroutine(obj.GetComponent<Growing>().Growth());
        Destroy(gameObject);
    }
}
