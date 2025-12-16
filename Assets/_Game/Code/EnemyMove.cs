using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]


public class EnemyMove : MonoBehaviour
{


    public float walkSpeed = 3f;  // Speed of the enemy
    Rigidbody2D rb;  // Reference to the Rigidbody2D component
    TouchingDirections touchingDirections;  // Reference to the TouchingDirections component
    public enum WalkableDirection { Left, Right }  //enum for walkable direction
    Animator animator;  // Reference to the Animator component
    private WalkableDirection _walkdirection;
    private Vector2 WalkDirectionVector = Vector2.right;  //default to right

    public EnemyDetectionZone detectionZone;




    public WalkableDirection WalkDirection  // Property for getting and setting walk direction
    {
        get { return _walkdirection; }
        set
        {
            if (_walkdirection != value)
            {
                gameObject.transform.localScale = new Vector2(WalkDirectionVector.x * -1, gameObject.transform.localScale.y); //flip the sprite

                if (value == WalkableDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        set
        {
            if (_hasTarget != value)
            {
                animator.SetBool(AnimationStrings.hasTarget, value);

            }

        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        detectionZone = GetComponentInChildren<EnemyDetectionZone>();
    }
    private void Update()
    {
      HasTarget= detectionZone.DetectedColliders.Count > 0;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void FixedUpdate()
    {
        if(touchingDirections.isGrounded && touchingDirections.isOnWall)
        {
            FlipDirection();
        }
        rb.linearVelocity = new Vector2(walkSpeed * WalkDirectionVector.x, rb.linearVelocity.y);
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Left;
        }
    }


    
}
