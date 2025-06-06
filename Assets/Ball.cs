using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody2D rb;
    private PongRallyTracker tracker;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tracker = FindObjectOfType<PongRallyTracker>();
        LaunchBall();
    }

    void LaunchBall()
    {
        rb.linearVelocity = new Vector2(-1f, Random.Range(-0.5f, 0.5f)).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Paddle"))
        {
            tracker.BallBounced();
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            // bounce automatically handled by Unity physics
        }
        else if (collision.collider.CompareTag("MissZone"))
        {
            tracker.BallMissed();
            transform.position = Vector2.zero;
            LaunchBall();
        }
    }
}
