using UnityEngine;

public class PongQuestGiver : MonoBehaviour
{
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!PongQuestManager.Instance.questAccepted)
            {
                PongQuestManager.Instance.questAccepted = true;
                Debug.Log("Quest Accepted: Get a rally of 10 in Pong!");
            }
            else
            {
                Debug.Log("You already accepted the quest.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }
}
