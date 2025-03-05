using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    public string Scene2;  // Name of the scene to teleport to
    public Transform targetDoor;    // The target door in the new scene
    public string targetDoorTag = "TeleportDoor";  // Tag of the target door

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the door trigger
        if (other.CompareTag("Player"))
        {
            // Load the target scene asynchronously (better performance)
            StartCoroutine(Teleport(other.transform));
        }
    }

    private System.Collections.IEnumerator Teleport(Transform player)
    {
       
        yield return SceneManager.LoadSceneAsync(Scene2, LoadSceneMode.Single);

        // Once the new scene is loaded, teleport the player to the target door
        player.position = targetDoor.position;

    }
}
