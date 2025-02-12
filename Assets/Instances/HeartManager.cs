using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public GameObject[] mesCoeurs;

    void Start() {
        mesCoeurs = new GameObject[transform.childCount];
        
        for(int i = 0; i < mesCoeurs.Length; i++) {
            mesCoeurs[i] = transform.GetChild(i).gameObject;
        }
    }

    public void updateCoeur(int vie) {
        for (int i = 0 ; i < mesCoeurs.Length ; i++) {
            if(i < vie) {
                mesCoeurs[i].SetActive(true);
            } else {
                mesCoeurs[i].SetActive(false);
            }
        }
    }
}
