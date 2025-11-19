using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

using static UnityEngine.Rendering.DebugUI;


public class PlayerMovement : MonoBehaviour
{
    private const float V = 0f;
    [SerializeField] float moveSpeed;    
    [SerializeField] float jumpForce; 
    [SerializeField] float climbingSpeed;
    private float moveInput;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;


    Rigidbody2D rb;
    Animator myAnimator;
    CapsuleCollider2D capsuleCollider;
    float gravityScaleAtStart;
    BoxCollider2D jumpCollider2D;
    bool isAlive = true;


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
        jumpCollider2D = GetComponent<BoxCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    // Update is called once per frame
   public void Update()
    {

        if (!isAlive) 
        {
            return; 
        }

        //Run(GetPVelocity());
        FlipPlayer();
        ClimbLadder();
        Die();


        // Get horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        if (jumpAction.IsPressed())
        {
            if (!jumpCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            else
            {
                if (jumpAction.IsPressed())
                {
                    rb.linearVelocity += new Vector2(V, jumpForce);
                }
            }
            
        }
        
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    /* void OnMove(InputValue value)  // Input System action callback to get movement input
     {
         if (!isAlive) 
         {
             return; 
         }
         movement = value.Get<Vector2>();  // Store movement input

     }

     private Vector2 GetPVelocity()
     {
         return new Vector2(movement.x * moveSpeed,  V);
     }

     void Run(Vector2 PVelocity)
     {
         rb.linearVelocity = movement;  // Apply movement to Rigidbody2D 
         bool hasHorizontalSpeed = Mathf.Abs((rb.linearVelocity.x)+V) > Mathf.Epsilon;
         myAnimator.SetBool("isRunning", hasHorizontalSpeed);  // Update animator based on horizontal speed
     }*/

    void FlipPlayer()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon; // Check if player is moving horizontally
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f); // Flip player sprite based on movement direction
        }
    }

    /*void OnJump(InputValue value)
    {
        if (!jumpCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            rb.linearVelocity += new Vector2(0f, jumpForce);
            
        } 
    }*/
    void ClimbLadder()
    {
        // Update animator based on vertical speed
        
        rb.gravityScale = 1f;
        if (!jumpCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            return;
        }
        else
        {
            Vector2 climbVelocity = new(rb.linearVelocity.x * moveSpeed, rb.linearVelocity.y * climbingSpeed);
            myAnimator.SetBool("isClimbing", Mathf.Abs(rb.linearVelocity.y) > Mathf.Epsilon);
            // Get current velocity
        } 
    }
    void Die()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
        }
    }

}
