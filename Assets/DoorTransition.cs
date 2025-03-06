using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTeleport : MonoBehaviour
{
    public string sceneToLoad;  // Name of the scene to teleport to
    public string targetDoorTag = "TeleportDoor";  // Tag of the target door in the new scene

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other.transform));

           
        }
    }

    private IEnumerator Teleport(Transform player)
    {
        //// Disable player movement (if there's a movement script)
        //PlayerController playerController = player.GetComponent<PlayerController>();
        //if (playerController != null)
        //{
        //    playerController.enabled = false;
        //}

        // Load the target scene
        yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        // Find the target door in the new scene
        GameObject targetDoor = GameObject.FindGameObjectWithTag(targetDoorTag);
        if (targetDoor != null)
        {
            player.position = targetDoor.transform.position;
        }
        else
        {
            Debug.LogWarning("No door found with tag: " + targetDoorTag);
        }

        //// Re-enable player movement
        //if (playerController != null)
        //{
        //    playerController.enabled = true;
        //}
    }
}
