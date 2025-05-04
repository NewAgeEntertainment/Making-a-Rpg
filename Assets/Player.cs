using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour 
{

    [Header("Attack details")]
    public float[] attackMovement; // Array to store the attack movement values

    public bool isBusy { get; private set; } // Boolean to track if the player is busy (e.g., during an attack)

    [Header("Move Info")]
    public float moveSpeed = 12f; // Speed of the player movement
    public float jumpForce; // Force applied when the player jumps

    [Header("Dash Info")]
    [SerializeField] private float dashCooldown; // Cooldown time for the dash ability
    private float dashUsageTimer; // Timer to track the dash cooldown
    public float dashSpeed; // Speed of the player dash
    public float dashDuration; // Duration of the dash
    public float dashDir { get; private set; } // Direction of the dash

    [Header("Collision Check")]
    [SerializeField] public Transform groundCheck; // Transform used to check if the player is on the ground
    [SerializeField] private float groundCheckDistance; // Distance used for ground check
    [SerializeField] private Transform wallCheck; // Transform used to check if the player is against a wall
    [SerializeField] private float wallCheckDistance; // Distance used for wall check
    [SerializeField] private LayerMask whatIsGround; // Layer mask used to identify the ground layer

    public PlayerDashState dashState { get; private set; } // Reference to the dash state

    public int facingDir { get; private set; } = 1; // Direction the player is facing (1 for right, -1 for left)
    private bool facingRight = true; // Boolean to track if the player is facing right

    #region Components
    public Animator anim { get; private set; } // Reference to the Animator component for animation control
    public Rigidbody2D rb { get; private set; } // Reference to the Rigidbody2D component for physics control
    #endregion

    #region State
    public PlayerStateMachine stateMachine; // Reference to the player's state machine
    public PlayerIdleState idleState { get; private set; } // Reference to the idle state
    public PlayerMoveState moveState { get; private set; } // Reference to the move state
    public PlayerAirState airState { get; private set; } // Reference to the air state
    public PlayerWallSlideState wallSlideState { get; private set; } // Reference to the wall slide state
    public PlayerWallJumpState wallJumpState { get; private set; } // Reference to the wall jump state
    public PlayerJumpState jumpState { get; private set; } // Reference to the jump state

    public PlayerPrimaryAttack primaryAttack { get; private set; } // Reference to the primary attack state
    #endregion


    private void Awake() // Awake is called when the script instance is being loaded
    {

        stateMachine = new PlayerStateMachine(); // Initialize the state machine
        
        idleState = new PlayerIdleState(this, stateMachine, "Idle"); // Create the idle state
        moveState = new PlayerMoveState(this, stateMachine, "Move"); // Create the move state
        airState = new PlayerAirState(this, stateMachine, "Jump"); // Create the air state
        jumpState = new PlayerJumpState(this, stateMachine, "Jump"); // Create the jump state
        dashState = new PlayerDashState(this, stateMachine, "Dash"); // Create the dash state
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide"); // Create the wall slide state
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump"); // Create the wall jump state
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack"); // Create the primary attack state
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>(); // Get the Animator component from the child GameObject
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the GameObject

        stateMachine.Initialize(idleState); // Initialize the state machine with the idle state
        

    }

    


    private void Update()
    {
        stateMachine.currentState.Update(); // Call the Update method of the current state

        CheckForDashInput(); // Check for dash input
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true; // Set the isBusy flag to true
        Debug.Log("IS BUSY");

        yield return new WaitForSeconds(_seconds); // Wait for the specified number of seconds

        Debug.Log("NOT BUSY");
        isBusy = false; // Set the isBusy flag to false
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger(); // Call the AnimationFinishTrigger method of the current state

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return; // If the player is against a wall, do not allow dashing




        dashUsageTimer -= Time.deltaTime; // Decrease the dash usage timer by the time since the last frame

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) // Check if the left shift key is pressed and the dash cooldown is over
        {
            dashUsageTimer = dashCooldown; // Reset the dash usage timer to the cooldown value
            
            dashDir = Input.GetAxisRaw("Horizontal"); // Get the horizontal input value for dash direction
            //dashDir = Input.GetAxisRaw("Vertical"); // Get the vertical input value for dash direction

            if (dashDir == 0) // If no input is detected, set the dash direction to the facing direction
                dashDir = facingDir;

            stateMachine.ChangeState(dashState); // Change to the dash state when the left shift key is pressed

        }
    }
    public void SetVelocity(float _xVelocity, float _yVelocity) // Method to set the velocity of the Rigidbody2D component
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity); // Set the velocity of the Rigidbody2D component
        FlipController(_xVelocity); // Call the FlipController method to check if the player needs to flip
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); // Method to check if the player is on the ground using a raycast
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround); // Method to check if the player is against a wall using a raycast
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance)); // Draw a line in the Scene view to visualize the ground check
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y)); // Draw a line in the Scene view to visualize the wall check
    }


    public void flip()
    {
        facingDir = facingDir * -1; // Flip the direction the player is facing
        facingRight = !facingRight; // Toggle the facingRight boolean
        transform.Rotate(0f, 180f, 0f); // Rotate the player GameObject by 180 degrees around the Y-axis
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            flip();
        else if (_x < 0 && facingRight)
            flip();
    }
}


//  #example of timer
//
//    public float Timer;
//    public float cooldown = 5;

//  Timer -= Time.deltaTime; // Increment the timer by the time since the last frame

//  if (Timer < 0 && Input.GetKeyDown(KeyCode.R)) // this method says if the R key is pressed set the timer to a positive value of coildown.
//  {
//    Timer = cooldown; // Reset the timer if the cooldown is not met
//  }

// dash in the direction without input Example
// if (dashDir == 0)
//    dashDir = FacingDir;
