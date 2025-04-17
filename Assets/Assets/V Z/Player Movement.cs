using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
   Rigidbody2D body;
   float horizontal;
   float vertical;
   float moveLimiter = 0.7f;

   public Animator animator;

   public Vector2 movement;

   public float runSpeed = 20.0f;

   void Start()
   {
    body = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
   }

   void Update ()
   {
    //fluxes between -1 and 1
    horizontal = Input.GetAxisRaw("Horizontal"); // -1 is to left
    vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    movement = new Vector2(horizontal, vertical);

    animator.SetFloat("horizontal", movement.x);
    animator.SetFloat("vertical", movement.y);
    animator.SetFloat("speed", movement.sqrMagnitude);

    if (movement != Vector2.zero) { //if player is moving
        animator.SetFloat("lastHoriz", movement.x); //remember horizontal direction
        animator.SetFloat("lastVert", movement.y); //remember vertical direction
    }
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
