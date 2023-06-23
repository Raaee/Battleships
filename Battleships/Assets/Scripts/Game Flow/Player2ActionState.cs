using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2ActionState : GameState {

    private Vector2 randomLocation;
    private CubeVisual currentCube;
    [SerializeField] PlacementData placementData;
    [SerializeField] GridManager playerGridMan;
    private bool hit = false;

    public override void OnStateEnter() {
        ChooseAttackLoc();
        CheckIfHit();
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
        hit = placementData.CheckIfHit(randomLocation);

        if (hit) {
            Debug.Log("Enemy Hit! " + randomLocation);
            currentCube.CubeHit();
        }
        else {
            Debug.Log("Enemy Missed.");
            currentCube.CubeMiss();
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
