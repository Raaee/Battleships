using System.Collections;
using UnityEngine;

public class Player1ActionState : GameState {
   // [SerializeField] private IActions playerActions;
    public override void OnStateEnter() {
       // playerActions.DetermineLocation();
    }
    public override void OnStateUpdate() {
    }

    public override void OnStateExit() {

    }
    public void SelectPowerUp() {
        Debug.Log("You selected a power-up.");
    }

    /* player 1
     * - if PU available && player choose -> use powerup , else -> nothing
     *    - show PU available (UI)
     * - use highlight system
     * - save location clicked
     * - show button for hit location once chosen
     * if button pressed:
     *    - checks if pawn hit -> remove pawn coord that was hit, else -> missed:
     *       -> button triggers visual feedback
     * - close highlight system
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
}