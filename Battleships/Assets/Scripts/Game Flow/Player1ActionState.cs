﻿using System.Collections;
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
        if (pawnHit)
            Debug.Log("Hit!");
        else
            Debug.Log("Missed.");
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

    /* player 2 (enemy)
     * - if PU available -> use, else -> nothing
     * - choose random location
     * - save location chosen
     * - checks if pawn hit -> remove pawn coord that was hit, else -> missed:
     *     -> button triggers visual feedback
     * - go to player 1 turn or other branch
     */
    
    /*
     *  On mouse down (cube visual)-> get the current highlighted cube in attack Highlight systme, get the tile position using the gridmanager in the attackHighlight
     *  use setter to save the position in Player1Action
     *   
     */
}