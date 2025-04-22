using UnityEngine;

public class ControlHint : MonoBehaviour
{
    public float displayDuration = 8f;  //  stays visible for 8 seconds
    public float fadeDuration = 1.5f;   // fades out over 1.5 seconds

    private CanvasGroup canvasGroup;
    private float timer = 0f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > displayDuration)
        {
            float fadeProgress = (timer - displayDuration) / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, fadeProgress);

            if (fadeProgress >= 1f)
            {
                Destroy(gameObject); // Completely remove after fade
            }
        }
    }
}
