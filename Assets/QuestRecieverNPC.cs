using UnityEngine;

public class QuestReceiverNPC : MonoBehaviour, INPV
{
    public void Talk()
    {
        if (QuestManager.Instance.questAccepted && !QuestManager.Instance.questCompleted)
        {
            Debug.Log("NPC: Thank you for coming! Quest complete.");
            QuestManager.Instance.questCompleted = true;
        }
        else if (QuestManager.Instance.questCompleted)
        {
            Debug.Log("NPC: Great job completing the quest!");
        }
        else
        {
            Debug.Log("NPC: Hello stranger.");
        }
    }
}
