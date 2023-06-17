using System.Collections;
using UnityEngine;

public class Player1ActionState : GameState {
   // [SerializeField] private IActions playerActions;
   private AttackHighlightSystem attackHighlightSystem;

   void Awake()
   {
       attackHighlightSystem = FindObjectOfType<AttackHighlightSystem>();
   }
    public override void OnStateEnter() {
       // playerActions.DetermineLocation();
       //Open Highlight System
       attackHighlightSystem.EnableSystem();
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
     * - use highlight system (DONE)
     * - save location clicked
     * - show button for hit location once chosen
     * if button pressed:
     *    - checks if pawn hit -> remove pawn coord that was hit, else -> missed: USE DEBUG
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
    
    /*
     *  On mouse down (cube visual)-> get the current highlighted cube in attack Highlight systme, get the tile position using the gridmanager in the attackHighlight
     *  use setter to save the position in Player1Action
     *   
     */
}