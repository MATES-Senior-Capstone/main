using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string sceneToLoad;  // The scene to load
    public string targetDoorTag = "TeleportDoor";  // The tag of the target door in the new scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the door trigger!");
            DontDestroyOnLoad(other.gameObject); // Keep the player object alive between scenes
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        // Load the new scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        // Wait until the scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return null; // Wait one extra frame for objects to initialize

        // Find the new player object in the scene (in case it's different)
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found in new scene!");
            yield break;
        }

        // Find the target door in the new scene
        GameObject targetDoor = GameObject.FindGameObjectWithTag(targetDoorTag);

        if (targetDoor != null)
        {
            player.transform.position = targetDoor.transform.position; // Move player to the new door
            Debug.Log("Teleported player to: " + player.transform.position);
        }
        else
        {
            Debug.LogError("Target door not found! Make sure it has the correct tag.");
        }
    }
}

