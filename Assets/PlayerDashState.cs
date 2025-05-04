using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState // This class represents the dash state of the player. It inherits from the PlayerState class.
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter(); // Call the base class Enter method from PlayerState script

        stateTimer = player.dashDuration; // Start the timer for the dash duration
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0f, 0); // Reset the player's velocity to 0 in the x direction when exiting the dash state
    }

    public override void Update()
    {
        base.Update(); // Call the base class Update method from PlayerState script

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState); // If the player is not on the ground and is against a wall, change to the wall slide state

        player.SetVelocity(player.dashSpeed * player.dashDir, rb.velocity.y); // Set the player's velocity to the dash speed in the direction the player is facing

        if (stateTimer < 0) // Check if the dash duration is over
            stateMachine.ChangeState(player.idleState); // Change to idle state if the dash duration is over
    }
}
