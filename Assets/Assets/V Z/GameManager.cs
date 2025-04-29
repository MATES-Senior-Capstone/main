using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string LastUsedDoor; // Stores the door ID

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
}
