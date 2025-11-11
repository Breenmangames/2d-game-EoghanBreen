using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Windows;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movement;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;    
    [SerializeField] float jumpForce; 
    [SerializeField] float climbingSpeed;
    Animator myAnimator;
    CapsuleCollider2D capsuleCollider2D;
    InputAction jumpAction;
    InputActionAsset inputActionAsset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        inputActionAsset = InputSystem.actions;
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipPlayer();
        ClimbLadder();

        if (jumpAction.IsPressed())
        {
            //if (!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            //{
            //    return;
            //}
            if (jumpAction.IsPressed())
            {
                rb.linearVelocity += new Vector2(0f, jumpForce);
            }
        }
        //OnJump();
    }

    void OnMove(InputValue value)  // Input System action callback to get movement input
    {
        movement = value.Get<Vector2>();  // Store movement input
         
    }

    void Run()
    {
        Vector2 pVelocity = new Vector2(movement.x *moveSpeed, rb.linearVelocity.y);  // Get current velocity
        rb.linearVelocity = movement;  // Apply movement to Rigidbody2D 
        bool hasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
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

    void OnJump(InputValue input)
    {
        if (!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(input.isPressed)
        {
            rb.linearVelocity += new Vector2(0f, jumpForce);
            
        } 
    }
    void ClimbLadder()
    {
        Vector2 climbVelocity = new Vector2(rb.linearVelocity.x * moveSpeed, movement.y *climbingSpeed);  // Get current velocity
        rb.linearVelocity = climbVelocity;  // Apply movement to Rigidbody2D 

        if (!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }
        
    }
}
