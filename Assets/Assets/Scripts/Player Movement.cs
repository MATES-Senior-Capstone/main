using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
   Rigidbody2D body;
   float horizontal;
   float vertical;
   float moveLimiter = 0.7f;

   public float runSpeed = 20.0f;

   void Start()
   {
    body = GetComponent<Rigidbody2D>();
   }

   void Update ()
   {
    //fluxes between -1 and 1
    horizontal = Input.GetAxisRaw("Horizontal"); // -1 is to left
    vertical = Input.GetAxisRaw("Vertical"); // -1 is down
   }

   void FixedUpdate()
   {
    if (horizontal !=0 && vertical !=0) // Checks for diagonal
    {
        horizontal *= moveLimiter;
        vertical *= moveLimiter;
    }
    body.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
   }
}
