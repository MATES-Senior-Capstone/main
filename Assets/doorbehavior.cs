using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    public string door;  // The scene you want to load
    public float interactRadius = 2f;  // Distance the player needs to be in to interact
    private bool playerInRange = false; // Track whether the player is in range of the door

    private void Update()
    {
        // Check for player input when they are in range
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(door);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player enters the door's range, set playerInRange to true
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the player leaves the door's range, set playerInRange to false
        if (other.CompareTag("John Square"))
        {
            playerInRange = false;
        }
    }
}
