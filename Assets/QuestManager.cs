using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public bool questAccepted = false;
    public bool questCompleted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
