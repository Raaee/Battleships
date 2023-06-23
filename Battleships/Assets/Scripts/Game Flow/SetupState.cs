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

    [SerializeField] private ButtonFunctions buttonFunctions;


    private void Awake()
    {
        base.Awake();
        buttonFunctions.OnPlayerConfirmPlacement.AddListener(GoToPlayer1State);
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
        Debug.Log("Exiting Setup State");
    }

    public void GoToPlayer1State() {
       
        
        gameManager.ChangeState(player1AS);
    }
    public void GoToPlayer2State() {
        gameManager.ChangeState(player2AS);
    }
}
