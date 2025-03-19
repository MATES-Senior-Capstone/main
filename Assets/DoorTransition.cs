using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string sceneToLoad;  // The new scene to load
    public Vector2 spawnPosition; // The exact position where the player will appear in the new scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the door trigger!");
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        // Store player's current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Disable player movement to prevent actions during the transition (if applicable)
        player.SetActive(false);

        // Load the new scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        // Wait until the new scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move player to the new scene
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneToLoad));

        // Set player's position to the predetermined spawn location
        player.transform.position = spawnPosition;
        Debug.Log("Teleported player to: " + spawnPosition);

        // Reactivate player
        player.SetActive(true);

        // Unload the old scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        Debug.Log("Unloaded previous scene: " + currentScene);
    }
}
