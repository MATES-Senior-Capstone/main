using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string sceneToLoad;  // The new scene to load
    public Vector2 spawnPosition; // The exact position where the player will appear in the new scene
    private bool playerInDoorZone = false; // Track if the player is near the door
    private GameObject player; // Store player reference

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoorZone = true;
            player = other.gameObject;
            Debug.Log("Press 'E' to use the door.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoorZone = false;
            Debug.Log("Left the door area.");
        }
    }

    private void Update()
    {
        if (playerInDoorZone && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        if (player == null) yield break;

        string currentScene = SceneManager.GetActiveScene().name;

        // Temporarily disable player movement
        player.SetActive(false);

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move player to the new scene
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneToLoad));

        // Set player position to predetermined coordinates
        player.transform.position = spawnPosition;
        Debug.Log("Teleported player to: " + spawnPosition);

        // Reactivate player
        player.SetActive(true);

        // Unload the previous scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }
}

