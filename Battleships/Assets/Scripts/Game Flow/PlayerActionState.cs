using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The state for the player's turn
/// </summary>
public class PlayerActionState : GameState {
   
    private AttackHighlightSystem attackHighlightSystem;
    private CubeVisual currentCubeCV;
    private List<CubeVisual> alreadyHitCubes;
    private Vector2 attackLocation;

    private ButtonFunctions buttonsFunctions;
    private bool attackSelected = false;
    private bool attackConfirmed = false;
    [SerializeField] private Button attackConfirmBtn;

    [SerializeField] private BetterEnemyPlacement enemyPlacementData;
    [SerializeField] PlayerPlacementData playerPlacementData;
    [SerializeField] private PlayerAnimationControl animControl;
   
    private bool pawnHit = false;
    public override void Awake() {
        base.Awake();
        stateTeam = StateTeam.PLAYER;
        teamSide = TeamSide.DUSKMARE; // DEFAULT: (change this value depending on what the player chooses in main menu)
        attackHighlightSystem = FindObjectOfType<AttackHighlightSystem>();
        buttonsFunctions = FindObjectOfType<ButtonFunctions>(); 
        buttonsFunctions.OnPlayerConfirmAttack.AddListener(ConfirmAttack);
        buttonsFunctions.OnPlayerConfirmAttack.AddListener(EndAttackConfirm);
        alreadyHitCubes = new List<CubeVisual>();
    }
    public override void OnStateEnter() {
       
       //Open Highlight System
        attackHighlightSystem.EnableSystem();
        attackSelected = false;
        attackConfirmed = false;
        pawnHit = false;
        animControl.IndicateWhoseTurn(teamSide, animControl.GetEnemyGeneralDimness());
    }
    public override void OnStateUpdate() {

    }
    void Update() {
        SetAttackConfirmBtn();
    }
    public override void OnStateExit() {

    }

    public void SetAttackLocation() {
        currentCubeCV = attackHighlightSystem.GetCurrentlyHighlighted();
        if (currentCubeCV == null) return; //this should happen if it tries to attack wrong gruid
        if(!alreadyHitCubes.Contains(currentCubeCV))
        {
            alreadyHitCubes.Add(currentCubeCV);
        }
        else
        {
            return;
        }
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
    public void ConfirmAttack() {
        attackConfirmed = true;
    }
    public void EndAttackConfirm() {
        attackHighlightSystem.DisableSystem();
        // Debug.Log("before chcekc if pawn hit" + attackLocation);
        CheckIfPawnHit();
    }
    public void CheckIfPawnHit() {
       // Debug.Log("attk Loc" + attackLocation);
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
        animControl.StartAttack(currentCubeCV.gameObject);
        animControl.ShowHitMissText(currentCubeCV.gameObject, hit, TeamSide.DUSKMARE);
        StartCoroutine(CompleteTurnDelay(3.5f));
    }
   
    public override void TurnComplete() {
        onTurnCompletion?.Invoke();
    }
    public override IEnumerator CompleteTurnDelay(float time) {
        yield return PeteHelper.GetWait(time);
        TurnComplete();
    }

}