using UnityEngine;
using Unity.Cinemachine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform FollowTarget;
    private CinemachineCamera vcam;

    // Use this for initialization
    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
            if (Player != null)
            {
                FollowTarget = Player.transform;
                vcam.Follow = FollowTarget;
            }
        }
    }
}
