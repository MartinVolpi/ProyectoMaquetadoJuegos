using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = .1f;
    [SerializeField] private float runSpeed = .2f;

    [Space]
    [SerializeField] private float xSpeed = 10f;

    [SerializeField] private float ySpeed = 250f;
    [SerializeField] private float minDistance = 0.1f;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundLayer;
    
    // Refs
    private Rigidbody2D rb2D;

    private SpriteRenderer rend;

    // private Variables
    private float speed;

    private float inputDeadZone = 0.1f;
    private int dir;
    private bool isMoving = false;
    private bool isRunning = false;
    private bool isGrounded = true;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        speed = walkSpeed;
    }

    private void Update()
    {
        // Manages Run mode
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToogleRunMode();
        }
        // Check if player is on the ground
        CheckPlayerGrounded();

        // Player's movement
        HandleMovement();

        // Player's Jump
        HandleJump();
    }

    private void CheckPlayerGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, minDistance, groundLayer);
    }

    // Activates or Deactivates the running mode
    private void ToogleRunMode()
    {
        isRunning = !isRunning;
        speed = isRunning ? runSpeed : walkSpeed;
    }

    // Manages the player's Jump
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded) return;

            rb2D.AddForce(new Vector2(xSpeed * dir, ySpeed));
        }
    }

    // Manages the player's left-right Movement
    private void HandleMovement()
    {
        if (Input.GetAxis("Horizontal") > inputDeadZone) // move to Right
        {
            rb2D.transform.Translate(new Vector2(speed, 0));
            rend.flipX = false;
            dir = 1;
            isMoving = true;
        }
        else if (Input.GetAxis("Horizontal") < -inputDeadZone) // move to Left
        {
            rb2D.transform.Translate(new Vector2(-speed, 0));
            rend.flipX = true;
            dir = -1;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}