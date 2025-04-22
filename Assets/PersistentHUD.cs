using UnityEngine;

public class PersistentHUD : MonoBehaviour
{
    public static PersistentHUD instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
