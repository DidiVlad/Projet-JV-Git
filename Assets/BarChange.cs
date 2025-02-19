using UnityEngine;
using UnityEngine.UI;

public class BarChange : MonoBehaviour
{
    public GameObject Boss; // Assign the Boss GameObject in the Inspector
    [SerializeField] private Image In;  // Red health bar (Fillable)
    [SerializeField] private Image Back; // Delayed effect bar
    public float LerpSpeed = 2f; // Speed for back bar delay

    private float BossMaxHP;
    private float BossHP;

    void Start()
    {
        if (Boss != null)
        {
            var health = Boss.GetComponent<HealthHandler>();
            BossMaxHP = health.MaxHP;
            BossHP = health.HP;
        }
    }

    void Update()
    {
        if (Boss == null) return;

        var health = Boss.GetComponent<HealthHandler>();
        BossMaxHP = health.MaxHP;
        BossHP = health.HP;

        float fillAmount = BossHP / BossMaxHP;

        // Instantly update the main HP bar (Red)
        In.fillAmount = fillAmount;

        // Smoothly lerp the back bar for a delay effect
        Back.fillAmount = Mathf.Lerp(Back.fillAmount, fillAmount, Time.deltaTime * LerpSpeed);
    }
}
