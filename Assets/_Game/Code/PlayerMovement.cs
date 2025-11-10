using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movement;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    Animator myAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipPlayer();
    }

    void OnMove(InputValue value)  // Input System action callback to get movement input
    {
        movement = value.Get<Vector2>();  // Store movement input
        Debug.Log("Movement Input: " + movement);  // Log movement input for debugging
    }

    void Run()
    {
        Vector2 pVelocity = new Vector2(movement.x *moveSpeed, rb.linearVelocity.y);  // Get current velocity
        rb.linearVelocity = movement;  // Apply movement to Rigidbody2D 

        myAnimator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon);  // Update animator based on horizontal speed
    }

    void FlipPlayer()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon; // Check if player is moving horizontally
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f); // Flip player sprite based on movement direction
        }
    }
}
