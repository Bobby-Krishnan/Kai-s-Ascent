using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private int totalEnemies = 0;

    [SerializeField] private string nextSceneName; // Set this in Inspector for each level
    [SerializeField] private GameObject victoryTextPrefab; // Assign in Level 4 only

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
            if (SceneManager.GetActiveScene().name == "Level4Scene")
            {
                ShowVictoryMessage();
            }
            else
            {
                Debug.Log("All enemies defeated! Loading next scene: " + nextSceneName);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private void UpdateEnemyCounterUI()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Enemies Left: " + totalEnemies;
        }
    }

    private void ShowVictoryMessage()
    {
        Debug.Log("Final level complete. Showing victory text.");

        GameObject hud = GameObject.Find("HUDCanvas");
        if (hud != null && victoryTextPrefab != null)
        {
            Instantiate(victoryTextPrefab, hud.transform);
        }

        Invoke("ReturnToTitleScreen", 10f);
    }

    private void ReturnToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreenScene");
    }
}
