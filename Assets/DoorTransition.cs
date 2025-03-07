using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string Scene2;  // Name of the scene to teleport to
    public string targetDoorTag = "TeleportDoor";  // Tag of the target door

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the door trigger!");
            StartCoroutine(Teleport(other.transform));
        }
    }

    private IEnumerator Teleport(Transform player)
    {
        // Load the new scene
        yield return SceneManager.LoadSceneAsync(Scene2, LoadSceneMode.Single);

        // Wait one frame to load
        yield return null;

        // Find the target door in the new scene
        GameObject newTargetDoor = GameObject.FindGameObjectWithTag(targetDoorTag);

        if (newTargetDoor != null)
        {
            player.position = newTargetDoor.transform.position;
        }
        else
        {
            Debug.LogError("Target door not found! Make sure it has the correct tag.");
        }
    }
}
