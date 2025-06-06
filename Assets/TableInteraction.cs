using UnityEngine;

public class TableInteraction : MonoBehaviour
{
    //public GameObject interactionPrompt;

    private bool playerInRange;

    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!PongQuestManager.Instance.questAccepted)
            {
                Debug.Log("You have no reason to use this yet.");
            }
            else if (!PongQuestManager.Instance.questCompleted)
            {
                Debug.Log("You still need to complete the Pong challenge.");
            }
            else
            {
                Debug.Log("You sit at the table.");
                // Trigger whatever table access event/animation/UI you want here
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            //interactionPrompt?.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            //interactionPrompt?.SetActive(false);
        }
    }
}
