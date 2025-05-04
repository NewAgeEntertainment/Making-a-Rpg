using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .4f; // Set the state timer to 0.4 seconds
        player.SetVelocity(5 * player.facingDir, player.jumpForce); // Set the player's velocity to a jump force in the direction they are facing
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) // Check if the state timer has reached zero
        {
            stateMachine.ChangeState(player.airState); // Change to the air state
        }

        if(player.IsGroundDetected()) // Check if the player is on the ground
        {
            stateMachine.ChangeState(player.idleState); // Change to the idle state
        }
    }
}
