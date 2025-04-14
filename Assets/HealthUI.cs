using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    private TextMeshProUGUI healthText;

    void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        PlayerHealth.HealthChanged += UpdateHealthText;
    }

    void OnDisable()
    {
        PlayerHealth.HealthChanged -= UpdateHealthText;
    }

    void UpdateHealthText(int current, int max)
    {
        healthText.text = "Health: " + current;
    }
}
