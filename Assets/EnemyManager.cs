using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private int totalEnemies = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
    }

    public void EnemyDefeated()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
        {
            Debug.Log("All enemies defeated! Returning to Title Screen...");
            SceneManager.LoadScene("TitleScreenScene");
        }
    }
}
