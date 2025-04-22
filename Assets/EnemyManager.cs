using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private int totalEnemies = 0;

    // Assign this in the Inspector per scene (e.g., "Level2Scene", "TitleScreenScene")
    [SerializeField] private string nextSceneName;

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
            Debug.Log("All enemies defeated! Loading " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
