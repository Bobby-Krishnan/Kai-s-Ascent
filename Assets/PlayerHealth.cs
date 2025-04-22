using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int maxHealth = 5;
    public int currentHealth;

    public delegate void OnHealthChanged(int current, int max);
    public static event OnHealthChanged HealthChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (currentHealth <= 0)
            {
                currentHealth = maxHealth;
            }

            Debug.Log("PlayerHealth initialized and made persistent.");
        }
        else
        {
            Debug.Log("Duplicate PlayerHealth detected and destroyed.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name + ", Player Health: " + currentHealth);

        if (scene.name == "TitleScreenScene")
        {
            currentHealth = maxHealth;
            HealthChanged?.Invoke(currentHealth, maxHealth);

            // Destroy HUD if it exists
            GameObject hud = GameObject.Find("HUDCanvas");
            if (hud != null)
            {
                Destroy(hud);
                Debug.Log("HUDCanvas destroyed on TitleScreenScene.");
            }

            // Destroy persistent player so it can be recreated fresh on new game
            Destroy(gameObject);
            Debug.Log("Player destroyed on returning to Title Screen.");
            return;
        }

        // Reposition player to spawn point
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            Debug.Log("Player repositioned to spawn point.");
        }
        else
        {
            Debug.LogWarning("No spawn point (tagged 'Respawn') found in scene.");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        HealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log("Player took damage. Current health: " + currentHealth);

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

        Debug.Log("Player healed. Current health: " + currentHealth);
    }

    public void Die()
    {
        Debug.Log("Player died!");
        SceneManager.LoadScene("TitleScreenScene");
    }
}
