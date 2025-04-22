using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3HintSpawner : MonoBehaviour
{
    public GameObject doubleJumpHintPrefab;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level3Scene")
        {
            GameObject hud = GameObject.Find("HUDCanvas");
            if (hud != null && doubleJumpHintPrefab != null)
            {
                Instantiate(doubleJumpHintPrefab, hud.transform);
                Debug.Log("Double Jump Hint spawned.");
            }
            else
            {
                Debug.LogWarning("Could not find HUDCanvas or hint prefab.");
            }
        }

        Destroy(gameObject); // cleanup after spawning
    }
}
