using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse0)) // Check if the left mouse button is pressed
            stateMachine.ChangeState(player.primaryAttack); // Change to the primary attack state when the left mouse button is pressed

        if (!player.IsGroundDetected()) // Check if the player is not on the ground
            stateMachine.ChangeState(player.airState); // Change to the air state if the player is not on the ground

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected()) // Check if the space key is pressed
            stateMachine.ChangeState(player.jumpState);  // Change to the jump state when the space key is pressed
    }
}
