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

   
    

  

   


    public override void Awake()
    {
        base.Awake();
       
      
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
    public override IEnumerator CompleteTurnDelay(float time) {
        yield return PeteHelper.GetWait(time);
    }

   
}
