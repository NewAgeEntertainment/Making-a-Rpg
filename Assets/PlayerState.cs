using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine; // Reference to the state machine that this state belongs to
    protected Player player; // Reference to the player object that this state controls

    protected Rigidbody2D rb; // Reference to the player's Rigidbody2D component for physics interactions

    protected float xInput; // Horizontal input value for player movement
    protected float yInput; // Vertical input value for player movement
    
    private string animBoolName; // Name of the animation bool to set when entering this state

    protected float stateTimer; // Timer to track the duration of the state
    protected bool triggerCalled; // Flag to check if the trigger has been called

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) // Constructor to Initialize the state with the player, state machine, and animation bool name
    {
        this.player = _player; // Assign the player object to this state
        this.stateMachine = _stateMachine; // Assign the state machine to this state
        this.animBoolName = _animeBoolName; // Assign the animation bool name to this state
    } 

    public virtual void Enter() // Method called when entering this state
    {
        player.anim.SetBool(animBoolName, true); // Set the animation bool to true to trigger the animation
        rb = player.rb; // Get the Rigidbody2D component from the player object
        triggerCalled = false; // Reset the trigger called flag
    }

    public virtual void Update() // Method called every frame while in this state
    {
        stateTimer -= Time.deltaTime; // Decrease the state timer by the time since the last frame
        xInput = Input.GetAxisRaw("Horizontal"); // Get the horizontal input value
        yInput = Input.GetAxisRaw("Vertical"); // Get the vertical input value

        player.anim.SetFloat("yVelocity", rb.velocity.y); // Set the yVelocity parameter in the animator to control the jump animation
    }
    

    public virtual void Exit() // Method called when exiting this state
    {
        player.anim.SetBool(animBoolName, false); // Set the animation bool to false to stop the animation
    }

    public virtual void AnimationFinishTrigger() // Method called when the animation finishes
    {
        triggerCalled = true; // Set the trigger called flag to true
    }

}
