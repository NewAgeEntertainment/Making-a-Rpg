using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
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

        if (player.IsWallDetected()) // Check if the player is against a wall
        {
            stateMachine.ChangeState(player.wallSlideState); // Change to the wall slide state if the player is against a wall
        }

        if (player.IsGroundDetected())  // Check if the player is on the ground
            stateMachine.ChangeState(player.idleState); // Change to the idle state when the player is on the ground

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y); // Set the player's velocity based on the horizontal input
        }

    }
}
