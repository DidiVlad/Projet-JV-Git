using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(DestroyParent);
    }

    void DestroyParent()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Debug.LogWarning("No parent found to destroy!");
        }
    }
}
