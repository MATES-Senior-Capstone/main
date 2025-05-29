using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 1.5f;
    public LayerMask npcLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius, npcLayer);
            foreach (Collider2D hit in hits)
            {
                INPV npc = hit.GetComponent<INPV>();
                if (npc != null)
                {
                    npc.Talk();
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
