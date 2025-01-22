using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy_To_Spawn;
    public GameObject player; 

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        Spawn();
    }


    void Spawn()
    {
        GameObject new_Enemy = Instantiate(Enemy_To_Spawn);
        new_Enemy.transform.position = gameObject.transform.position;
        new_Enemy.GetComponent<IA_Main>().plr = player;
    }

}
