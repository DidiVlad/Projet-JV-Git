using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthMain : MonoBehaviour
{
    public float HP = 3f;
    public bool CanTakeDamage = true;
    public GameObject Lastcheckpoint;
    public GameObject Spawners_parent;
    public GameObject EnemiesHolder;

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        if (CheckForHP())
        {
            ResetPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {  
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Projectile"))
        {
           if (CanTakeDamage)
          {
              CanTakeDamage = false;
              HP -= 1;
              gameObject.GetComponent<SpriteRenderer>().color = Color.red;
             StartCoroutine(ResetDamageCooldown());
          }    
      }
      else
      {
        //print(other.name);
      }
    }
}

    IEnumerator ResetDamageCooldown()
    {
    yield return new WaitForSeconds(1.5f);
     CanTakeDamage = true; 
     gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    bool CheckForHP()
    {
        return HP <= 0;
    }

    private void ResetPlayer()
    {
        HP = 3;
        gameObject.transform.position = Lastcheckpoint.transform.position;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        print("triggered");
        foreach (Transform enemies in EnemiesHolder.transform)
        {
            Destroy(enemies.gameObject);
        }
        for (int i = 0; i < Spawners_parent.transform.childCount; i++)
        {
        Transform Child = Spawners_parent.transform.GetChild(i);
        Child.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        GameObject new_Enemy = Instantiate(Child.GetComponent<Spawner>().Enemy_To_Spawn);
        new_Enemy.transform.position = Child.transform.position;
        new_Enemy.transform.parent = EnemiesHolder.transform;
        new_Enemy.GetComponent<IA_Main>().plr = gameObject;
        }

    }

}