using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The gamemanager, which is currently being run as a state machine. Each state is an inherited abstract "GameState"
/// </summary>
public class GameManager : MonoBehaviour
{
   

    [Header("Game Components")]
    [SerializeField] private BetterEnemyPlacement enemyPD;
    [SerializeField] private PlayerPlacementData playerPD;

    [SerializeField] private GameState initialState;
    [SerializeField] private GameState gameOverState;
    [SerializeField] private GameState player1AS;
    [SerializeField] private GameState player2AS;

    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] int maximumRounds = 29;

    
    [SerializeField] private ButtonFunctions buttonFunctions;
    
    [Header("Debug")]
    [SerializeField] private GameState currentState;
    [SerializeField] int currentRound = 0;   

    private void Awake() {
        currentState = initialState;
        buttonFunctions.OnPlayerConfirmPlacement.AddListener(GoToPlayer1State);
        player1AS.onTurnCompletion.AddListener(GoToPlayer2State);
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
        UpdateUI();
    }

    public void UpdateUI() {
        gameplayUI.SetCurrentState(currentState);
        gameplayUI.UpdateTurnTxt();
        gameplayUI.UpdateRoundNum(currentRound, maximumRounds);
        gameplayUI.UpdateShipsRemainTxt(enemyPD.GetNumOfPawnsInBattle());
    }

   
    public bool CheckRoundForGameOver()
    {
        //first check if over max rounds 
        if (currentRound > maximumRounds)
            return true;

        Debug.Log("enemy pawns left " + enemyPD.GetNumOfPawnsInBattle());
        Debug.Log("player pawns left " + playerPD.GetNumOfPawnsInBattle());
        //now we check if any of the teams have no ships
        if (playerPD.GetNumOfPawnsInBattle() <= 0)
        {
            return true;

        }

        if (enemyPD.GetNumOfPawnsInBattle() <= 0)
        {
            return true;

        }


        currentRound++;
        return false;
    }

    public void GoToPlayer1State()
    {
        if (CheckRoundForGameOver())
        {
            //go to game over state 
            ChangeState(gameOverState);
            return;
        }


        ChangeState(player1AS);
    }
    public void GoToPlayer2State()
    {
        if (CheckRoundForGameOver())
        {
            //go to game over state 
            ChangeState(gameOverState);
            return;
        }
        ChangeState(player2AS);
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
