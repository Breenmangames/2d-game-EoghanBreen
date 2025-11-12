using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Windows;
using static UnityEngine.Rendering.DebugUI;


public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField] float moveSpeed;    
    [SerializeField] float jumpForce; 
    [SerializeField] float climbingSpeed;

    Vector2 movement;
    Rigidbody2D rb;
    Animator myAnimator;
    CapsuleCollider2D capsuleCollider;
    float gravityScaleAtStart;

    InputAction AttackAction;
    InputAction jumpAction;
    InputActionAsset inputActionAsset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = rb.gravityScale;
        myAnimator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        inputActionAsset = InputSystem.actions;
        jumpAction = InputSystem.actions.FindAction("Jump");
        AttackAction = InputSystem.actions.FindAction("Attack");

    }

    // Update is called once per frame
   public void Update()
    {
        Run();
        FlipPlayer();
        ClimbLadder();

        if (jumpAction.IsPressed())
        {
            if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            if (jumpAction.IsPressed())
            {
                rb.linearVelocity += new Vector2(0f, jumpForce);
            }
        }
        
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

    void OnJump(InputValue value)
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            rb.linearVelocity += new Vector2(0f, jumpForce);
            
        } 
    }
    void ClimbLadder()
    {
        Vector2 climbVelocity = new Vector2(rb.linearVelocity.x * moveSpeed, movement.y *climbingSpeed);  // Get current velocity
        rb.linearVelocity = climbVelocity;  // Apply movement to Rigidbody2D
                                            // 
        rb.gravityScale = 1f;
        myAnimator.SetBool("isClimbing", Mathf.Abs(rb.linearVelocity.y) > Mathf.Epsilon);  // Update animator based on vertical speed

        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            return;
        }
        
    }
}
