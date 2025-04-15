using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private SlimeEnemy slimeEnemy;

    void Start()
    {
        currentHealth = maxHealth;
        slimeEnemy = GetComponent<SlimeEnemy>();

        if (slimeEnemy != null)
            EnemyManager.instance?.RegisterEnemy();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            slimeEnemy?.Die();
            EnemyManager.instance?.EnemyDefeated();
        }
    }
}
