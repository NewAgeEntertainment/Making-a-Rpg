using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; } // Property to get the current state of the player
    

    public void Initialize(PlayerState _startState) // Method to initialize the state machine with a starting state
    {
        currentState = _startState; // Set the current state to the starting state
        currentState.Enter(); // Call the Enter method of the starting state
    }

    public void ChangeState(PlayerState _newState) // Method to change the current state of the player
    {
        currentState.Exit(); // Call the Exit method of the current state
        currentState = _newState; // Set the current state to the new state
        currentState.Enter(); // Call the Enter method of the new state
    }
}
