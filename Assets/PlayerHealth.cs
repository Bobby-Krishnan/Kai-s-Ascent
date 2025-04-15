using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public delegate void OnHealthChanged(int current, int max);
    public static event OnHealthChanged HealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        HealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        HealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        HealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void Die()
    {
        Debug.Log("Player died!");
        // goes back to title screen, change later for levels
        SceneManager.LoadScene("TitleScreenScene");
    }
}
