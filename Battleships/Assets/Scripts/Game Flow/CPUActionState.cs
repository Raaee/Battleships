using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The computer's turn and decision state.  
/// </summary>
public class CPUActionState : GameState {

    private Vector2 randomLocation;
    private CubeVisual currentCube;
    [SerializeField] PlayerPlacementData playerPlacementData;
    [SerializeField] GridManager playerGridMan;
    private bool hit = false;

    [SerializeField] private GameState player1AS;

    public override void OnStateEnter() {
        ChooseAttackLoc();
        CheckIfHit();
        StartCoroutine(EndTurnAfterTime(2.5f));
    }
    private IEnumerator EndTurnAfterTime(float time) {
        Debug.Log("Waiting...");
        yield return PeteHelper.GetWait(time);
        gameManager.ChangeState(player1AS);
    }

    public override void OnStateUpdate() {

    }
    public override void OnStateExit() {

    }


    public void ChooseAttackLoc() {
        randomLocation = FindObjectOfType<BetterEnemyPlacement>().GetRandomVector2(0, 10, 0, 10);
        currentCube = playerGridMan.GetTileAtPosition(randomLocation).GetComponent<CubeVisual>();
    }
    public void CheckIfHit() {
        hit = playerPlacementData.CheckIfHit(randomLocation);

        if (hit) {
            Debug.Log("Enemy Hit! " + randomLocation);
            currentCube.ChangeMaterialOnHitState(CubeHitState.HIT);
        }
        else {
            Debug.Log("Enemy Missed.");
            currentCube.ChangeMaterialOnHitState(CubeHitState.MISS);
        }

        ShowHitFeedback(hit);
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


    /* player 2 (enemy)
     * - if PU available -> use, else -> nothing
     * - choose random location (DONE)
     * - save location chosen (DONE)
     * - checks if pawn hit -> remove pawn coord that was hit, else -> missed:
     *     -> button triggers visual feedback (DONE)
     * - go to player 1 turn or other branch
     */

    /*
     *  On mouse down (cube visual)-> get the current highlighted cube in attack Highlight systme, get the tile position using the gridmanager in the attackHighlight
     *  use setter to save the position in Player1Action
     *   
     */
}
