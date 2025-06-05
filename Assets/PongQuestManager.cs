using UnityEngine;

public class PongQuestManager : MonoBehaviour
{
    public static PongQuestManager Instance;

    public bool questAccepted = false;
    public bool questCompleted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteQuest()
    {
        questCompleted = true;
    }
}
