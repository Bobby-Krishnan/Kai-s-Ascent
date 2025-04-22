using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private int totalEnemies = 0;

    [SerializeField] private string nextSceneName; // Set this in Inspector for each level

    private TextMeshProUGUI enemyCounterText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameObject textObject = GameObject.Find("EnemyCounterText");
        if (textObject != null)
        {
            enemyCounterText = textObject.GetComponent<TextMeshProUGUI>();
            UpdateEnemyCounterUI();
        }
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
        UpdateEnemyCounterUI();
    }

    public void EnemyDefeated()
    {
        totalEnemies--;
        UpdateEnemyCounterUI();

        if (totalEnemies <= 0)
        {
            Debug.Log("All enemies defeated! Loading next scene: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void UpdateEnemyCounterUI()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Enemies Left: " + totalEnemies;
        }
    }
}
