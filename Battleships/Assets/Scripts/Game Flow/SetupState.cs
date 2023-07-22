using System.Collections;
using UnityEngine;
/// <summary>
/// The initialization state, when the game starts.
/// </summary>
public class SetupState : GameState 
{

    [SerializeField] private GridManager playerGM;
    [SerializeField] private GridManager EnemyGM;
    [SerializeField] private PlayerPlacementData playerPD;
    [SerializeField] private BetterEnemyPlacement enemyPD;

    [SerializeField] private GameState player1AS;
    [SerializeField] private GameState player2AS;

    [SerializeField] private GameState gameOverState;

    [SerializeField] private ButtonFunctions buttonFunctions;


    private void Awake()
    {
        base.Awake();
        buttonFunctions.OnPlayerConfirmPlacement.AddListener(GoToPlayer1State);
        player1AS.onTurnCompletion.AddListener(GoToPlayer2State);
       // buttonFunctions.OnPlayerConfirmAttack.AddListener(GoToPlayer2State);
    }
    public override void OnStateEnter() {
        playerGM.GenerateGrid();
        EnemyGM.GenerateGrid();
        playerPD.StartPlacement();
        enemyPD.StartPlacement();
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() {
       // Debug.Log("Exiting Setup State");
    }
    public override void TurnComplete() {
    
    }
    public override IEnumerator WaitForSec(float time) {
        yield return PeteHelper.GetWait(time);
    }

    public void GoToPlayer1State() {
        if(gameManager.CheckRoundForGameOver())
        {
            //go to game over state 
            gameManager.ChangeState(gameOverState);
            return;
        }
        

        gameManager.ChangeState(player1AS);
    }
    public void GoToPlayer2State() {
        if (gameManager.CheckRoundForGameOver())
        {
            //go to game over state 
            gameManager.ChangeState(gameOverState);
            return;
        }
        gameManager.ChangeState(player2AS);
    }
}
