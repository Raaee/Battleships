using System.Collections;
using UnityEngine;

public class SetupState : GameState {

    [SerializeField] private GridManager playerGM;
    [SerializeField] private GridManager EnemyGM;
    [SerializeField] private PlacementData playerPD;
    [SerializeField] private BetterEnemyPlacement enemyPD;

    [SerializeField] private GameState player1AS;

    public override void OnStateEnter() {
        playerGM.GenerateGrid();
        EnemyGM.GenerateGrid();
        playerPD.StartPlacement();
        enemyPD.StartPlacement();
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() {
        Debug.Log("Exit State");
    }

    public void GoToPlayer1State() {
        gameManager.ChangeState(player1AS);
    }
}
