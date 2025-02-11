using UnityEngine; 

public class Player_Shoot : MonoBehaviour
{
    public Transform Pinceau;
    public GameObject Peinture;
    Vector2 direction;
    public float Peinture_Vitesse;
    public Transform ShootPoint;
    public bool Shoot_debounce = false;
    public float Shoot_cooldown = 0.5f;
    public float Last_used_time;

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos-(Vector2)Pinceau.position;
        FaceMouse();

        if(Time.time > Last_used_time + Shoot_cooldown)
            {
                Shoot_debounce = false;
            }

        if (Input.GetMouseButtonDown(0) && Shoot_debounce == false )
        {
            Shoot_debounce = true;
            Last_used_time = Time.time;
            Lance_Peinture();
  
        }
    }

    void FaceMouse()
    {
        Pinceau.transform.right = direction;
    }

    void Lance_Peinture()
    {
        GameObject Nv_Peinture = Instantiate(Peinture,ShootPoint.position,ShootPoint.rotation);
        Nv_Peinture.GetComponent<Rigidbody2D>().AddForce(Nv_Peinture.transform.right * Peinture_Vitesse);

    }


}