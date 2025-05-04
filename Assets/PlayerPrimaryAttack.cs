using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{

    private int comboCounter; // Counter to track the number of combos performed

    private float lastTimeAttacked; // Timer to track the last time the player attacked
    private float comboWindow = 2; // Time window to allow for combo attacks
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) // Check if the combo counter exceeds 2 or if the time since the last attack exceeds the combo window
            comboCounter = 0; // Reset the combo counter if it exceeds 2

        player.anim.SetInteger("ComboCounter", comboCounter); // Set the combo counter in the animator to trigger the appropriate animation

        player.SetVelocity(player.attackMovement[comboCounter] * player.facingDir, rb.velocity.y);

        stateTimer = .1f; // Set the state timer to 1 second for the attack animation duration

        Debug.Log(comboCounter); // Log the current combo counter
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f); // Start a coroutine to make the player busy for a short duration after the attack

        comboCounter++; // Increment the combo counter when exiting the attack state
        lastTimeAttacked = Time.time; // Reset the last time attacked to the current time
        Debug.Log(lastTimeAttacked); // Log the last time attacked
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            rb.velocity = new Vector2(0, 0); // Stop the player's movement when the attack animation is finished
        
        

        if (triggerCalled) // Check if the animation has finished
        {
            stateMachine.ChangeState(player.idleState); // Change to the idle state when the animation is finished
        }
    }
}
