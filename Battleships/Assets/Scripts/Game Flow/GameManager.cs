using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The gamemanager, which is currently being run as a state machine. Each state is an inherited abstract "GameState"
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private BetterEnemyPlacement enemyPD;
    [SerializeField] private GameState initialState;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] int maximumRounds = 50;

    [Header("Debug")]
    [SerializeField] private GameState currentState;
    [SerializeField] int currentRound = 0;


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
        currentRound++;
        UpdateUI();
    }
    public void UpdateUI() {
        gameplayUI.SetCurrentState(currentState);
        gameplayUI.UpdateTurnTxt();
        gameplayUI.UpdateRoundNum(currentRound, maximumRounds);
        gameplayUI.UpdateShipsRemainTxt(enemyPD.pawnsInBattle.Count);
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
