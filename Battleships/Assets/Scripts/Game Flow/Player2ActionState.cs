using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2ActionState : GameState {

    private Vector2 randomLocation;
    [SerializeField] PlacementData placementData;
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
    }
    public void CheckIfHit() {
        hit = placementData.CheckIfHit(randomLocation);
        if (hit)
            Debug.Log("Enemy Hit! " + randomLocation);
        else
            Debug.Log("Enemy Missed.");
    }


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
