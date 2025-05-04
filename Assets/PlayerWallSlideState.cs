using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) // Check if the space key is pressed
        {
            stateMachine.ChangeState(player.wallJumpState); // Change to the wall jump state
            return; // Exit the update method to prevent further processing

        }

        if (xInput != 0 && player.facingDir != xInput) // Check if the player is trying to move in the opposite direction
            stateMachine.ChangeState(player.idleState); // Change to the idle state if the player is not facing the wall
        if (yInput < 0)
            
            rb.velocity = new Vector2(0, rb.velocity.y); // Apply a downward force to the player while sliding down the wall
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f); // Apply a downward force to the player while sliding down the wall

        if (player.IsGroundDetected())
                    stateMachine.ChangeState(player.idleState); // Change to the idle state if the player is not facing the wall

    }    
}
