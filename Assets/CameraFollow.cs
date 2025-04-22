using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        if (player == null)
        {
            GameObject found = GameObject.FindWithTag("Player");
            if (found != null) player = found.transform;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        float clampedX = Mathf.Clamp(smoothedPosition.x, leftLimit, rightLimit);
        float clampedY = Mathf.Clamp(smoothedPosition.y, bottomLimit, topLimit);

        transform.position = new Vector3(clampedX, clampedY, -10f);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject found = GameObject.FindWithTag("Player");
        if (found != null)
        {
            player = found.transform;
            Debug.Log("CameraFollow: Player found after scene load.");
        }
        else
        {
            Debug.LogWarning("CameraFollow: No player found after scene load.");
        }
    }
}
