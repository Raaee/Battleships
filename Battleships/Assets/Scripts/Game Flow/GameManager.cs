using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The gamemanager, which is currently being run as a state machine. Each state is an inherited abstract "GameState"
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState initialState;
    [Header("Debug")]
    [SerializeField] private GameState currentState;

    private void Awake() {
        currentState = initialState;
    }

    private void Start()
    {
        initialState.OnStateEnter();

    }

    private void Update() {
        currentState.OnStateUpdate();
    }

    public void ChangeState(GameState newState) {
        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }

    public GameState GetInitialState()
    {
        return initialState;
    }
    
    public GameState GetCurrentState()
    {
        return currentState;
    }
}
