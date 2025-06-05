using UnityEngine;

public class PongQuestGiver : MonoBehaviour
{
    public GameObject questPopupUI;

    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PongQuestManager.Instance.questAccepted = true;
            questPopupUI.SetActive(true); // Optional UI: “Try to rally the ball 10 times!”
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
        questPopupUI.SetActive(false);
    }
}
