using UnityEngine;
using UnityEngine.SceneManagement;

public class PongTableEntrance : MonoBehaviour
{
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PongQuestManager.Instance.questAccepted)
            {
                SceneManager.LoadScene("PongMinigame");
            }
            else
            {
                Debug.Log("You have no reason to play this right now.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}
