using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string sceneToLoad;  // Scene to load
    public Vector2 spawnPosition; // Exact position where player will appear
    public Image fadeImage; // UI Image for fade effect
    public float fadeDuration = 1f; // Duration of the fade effect

    private bool playerInDoorZone = false;
    private GameObject player;

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
        player.SetActive(false); // Temporarily disable player

        yield return StartCoroutine(FadeToBlack()); // Fade out before teleporting

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move player to the new scene
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneToLoad));
        player.transform.position = spawnPosition;
        Debug.Log("Teleported player to: " + spawnPosition);

        yield return new WaitForSeconds(0.2f); // Small delay before fading back in

        yield return StartCoroutine(FadeFromBlack()); // Fade in after teleporting

        player.SetActive(true); // Reactivate player

        // Unload the previous scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator FadeToBlack()
    {
        if (fadeImage == null) yield break;

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    private IEnumerator FadeFromBlack()
    {
        if (fadeImage == null) yield break;

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }
}

