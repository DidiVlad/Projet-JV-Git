using UnityEngine;

public class AppearMenu : MonoBehaviour
{
    public Canvas MenuUi; // Assign this in the Inspector

    void Start()
    {
        // Hide the menu when the game starts
        MenuUi.gameObject.SetActive(true);
    }

}
