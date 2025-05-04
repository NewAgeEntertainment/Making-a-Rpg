using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(0,0);  // Set the player's velocity to zero when entering the idle state

        
    
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            return; // If the player is trying to move in the same direction as the wall, do nothing

        if (xInput != 0 && !player.isBusy) // Check if the player is moving
            stateMachine.ChangeState(player.moveState); // Change to the move state when W is pressed
        //if (yInput != 0)
         //   stateMachine.ChangeState(player.moveState); // Change to the move state when S is pressed
    }
}
