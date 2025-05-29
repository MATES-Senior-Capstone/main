using UnityEngine;

public class QuestGiverNPC : MonoBehaviour, INPV
{
    public void Talk()
    {
        if (!QuestManager.Instance.questAccepted)
        {
            Debug.Log("NPC: Please find my friend in the other town.");
            QuestManager.Instance.questAccepted = true;
        }
        else
        {
            Debug.Log("NPC: Have you found my friend yet?");
        }
    }
}
