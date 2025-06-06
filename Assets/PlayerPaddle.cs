using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    public float speed = 8f;
    public string inputAxis = "Vertical";
    public Rigidbody2D rb;

    private void Update()
    {
        float move = Input.GetAxisRaw(inputAxis);
        rb.linearVelocity = new Vector2(0, move * speed);
    }
}
