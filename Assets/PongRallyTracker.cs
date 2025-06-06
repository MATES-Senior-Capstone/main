using UnityEngine;
using UnityEngine.SceneManagement;

public class PongRallyTracker : MonoBehaviour
{
    public int rallyTarget = 10;
    private int rallyCount = 0;

    public void BallBounced()
    {
        rallyCount++;
        Debug.Log("Rally: " + rallyCount);

        if (rallyCount >= rallyTarget)
        {
            Debug.Log("Quest Complete!");
            PongQuestManager.Instance.CompleteQuest();
            SceneManager.LoadScene("Gymnasium"); // Change to your main scene name
        }
    }

    public void BallMissed()
    {
        rallyCount = 0;
        Debug.Log("Missed. Rally reset.");
    }
}
