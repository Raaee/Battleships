using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player1ActionState : GameState {
   // [SerializeField] private IActions playerActions;
    private AttackHighlightSystem attackHighlightSystem;
    private CubeVisual currentCube;
    private Vector2 attackLocation;

    private ButtonFunctions buttonsFunctions;
    private bool attackSelected = false;
    private bool attackConfirmed = false;
    [SerializeField] private Button attackConfirmBtn;

    [SerializeField] private BetterEnemyPlacement enemyPlacementData;
    private bool pawnHit = false;

    void Awake() {
        attackHighlightSystem = FindObjectOfType<AttackHighlightSystem>();
        buttonsFunctions = FindObjectOfType<ButtonFunctions>(); 
        buttonsFunctions.OnPlayerConfirmAttack.AddListener(ConfirmAttack);
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
        AttackConfirmBtn();
    }
    public override void OnStateExit() {

    }
    public void SelectPowerUp() {
        Debug.Log("You selected a power-up.");
    }
    public void GetAttackLocation() {
        currentCube = attackHighlightSystem.GetCurrentlyHighlighted();
        attackLocation = attackHighlightSystem.GetCurrentAttackLocation();
        attackSelected = true;
        Debug.Log(attackLocation);
    }
    public void AttackConfirmBtn() {
        if (attackSelected && !attackConfirmed) {
            attackConfirmBtn.gameObject.SetActive(true);
        } else {
            attackConfirmBtn.gameObject.SetActive(false);
        }
    }
    public void EndAttackConfirm() {
        attackHighlightSystem.DisableSystem();
        CheckIfPawnHit();
    }
    public void ConfirmAttack() {
        attackConfirmed = true;
    }
    public void CheckIfPawnHit() {
        pawnHit = enemyPlacementData.CheckIfHit(attackLocation);

        if (pawnHit) {
            Debug.Log("You Hit!");
            currentCube.CubeHit();
        }
        else {
            Debug.Log("You Missed.");
            currentCube.CubeMiss();
        }

        ShowHitFeedback(pawnHit);
    }


    public void ShowHitFeedback(bool hit) {
        Debug.Log("***** Hit feedback goes here. *****");
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