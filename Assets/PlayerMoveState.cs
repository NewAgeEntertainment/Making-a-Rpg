using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
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

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y); // Set the player's velocity based on input

        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState); // Change to the idle state when M is pressed
        //if (yInput == 0)
         //   stateMachine.ChangeState(player.idleState); // Change to the idle state when S is pressed
    }
}

    
        //if (player.IsWallDetected())
        //    stateMachine.ChangeState(player.idleState); // Change to the idle state if the player is against a wall


