using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The state for the player's turn
/// </summary>
public class PlayerActionState : GameState {
   
    private AttackHighlightSystem attackHighlightSystem;
    private CubeVisual currentCubeCV;
    private Vector2 attackLocation;

    private ButtonFunctions buttonsFunctions;
    private bool attackSelected = false;
    private bool attackConfirmed = false;
    [SerializeField] private Button attackConfirmBtn;

    [SerializeField] private BetterEnemyPlacement enemyPlacementData;
    private bool pawnHit = false;

    void Awake() {
        base.Awake();
        stateTeam = StateTeam.PLAYER;
        attackHighlightSystem = FindObjectOfType<AttackHighlightSystem>();
        buttonsFunctions = FindObjectOfType<ButtonFunctions>(); 
        buttonsFunctions.OnPlayerConfirmAttack.AddListener(ConfirmAttack);
        buttonsFunctions.OnPlayerConfirmAttack.AddListener(EndAttackConfirm);
    }
    public override void OnStateEnter() {
       // playerActions.DetermineLocation();
       //Open Highlight System
        attackHighlightSystem.EnableSystem();
        attackSelected = false;
        attackConfirmed = false;
        pawnHit = false;
    }
    public override void OnStateUpdate() {

    }
    void Update() {
        SetAttackConfirmBtn();
    }
    public override void OnStateExit() {

    }
    public void SelectPowerUp() {
        Debug.Log("You selected a power-up.");
    }
    public void SetAttackLocation() {
        currentCubeCV = attackHighlightSystem.GetCurrentlyHighlighted();
        if (currentCubeCV == null) return; //this should happen if it tries to attack wrong gruid
        currentCubeCV.ShowHitMarkerIcon();
        attackLocation = attackHighlightSystem.GetCurrentAttackLocation();
        attackSelected = true;
    }
    public void SetAttackConfirmBtn() {
        if (attackSelected && !attackConfirmed) {
            attackConfirmBtn.gameObject.SetActive(true);
        } else {
            attackConfirmBtn.gameObject.SetActive(false);
        }
    }
    public void EndAttackConfirm() {
        attackHighlightSystem.DisableSystem();
       // Debug.Log("before chcekc if pawn hit" + attackLocation);
        CheckIfPawnHit();
    }
    public void ConfirmAttack() {
        attackConfirmed = true;
    }
    public void CheckIfPawnHit() {
        pawnHit = enemyPlacementData.CheckIfHit(attackLocation);

        if (pawnHit) {
            //Debug.Log("You Hit! pete");
            currentCubeCV.ChangeMaterialOnHitState(CubeHitState.HIT);
            enemyPlacementData.RemovePawnCoord(attackLocation);
        }
        else {
           // Debug.Log("You Missed. pete");
            currentCubeCV.ChangeMaterialOnHitState(CubeHitState.MISS);
        }

        ShowHitFeedback(pawnHit);
    }


    public void ShowHitFeedback(bool hit) {
      //  Debug.Log("***** Hit feedback goes here. *****");
        // shows hit feedback after attacking.

        // flow of feedback:
        // dragon static --> spit out anim
        // VFX + anim emitted near dragon mouth going up
        // SCREEN SHAKE
        // after few seconds, anim of projectile going down from above, ON the cube that was selected
        // (CHANGE CUBE COLOR TO SHOW ALREADY SELECTED)
        // call hit/miss popup text on cubevisual (this should be at the same time as the projectile hits the cube)
    }

    /* player 1
     * - if PU available && player choose -> use powerup , else -> nothing
     *    - show PU available (UI)
     * - use highlight system (DONE)
     * - save location clicked (DONE)
     * - show button for hit location once chosen (DONE)
     * if button pressed:
     *    - checks if pawn hit -> remove pawn coord that was hit, else -> missed: USE DEBUG (DONE)
     *       -> button triggers visual feedback **** <<<<<<
     * - close highlight system (DONE)
     * - go to player 2 turn or other branch
     */
}