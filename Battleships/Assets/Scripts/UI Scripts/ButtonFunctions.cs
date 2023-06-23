using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// the functions for the confirm attack and confirming placement 
/// </summary>
public class ButtonFunctions : MonoBehaviour { 
    public UnityEvent OnPlayerConfirmPlacement;
    public UnityEvent OnPlayerConfirmAttack;
    private bool playerConfirmedPlacement = false;

    public void PlacementConfirmation() {
        Debug.Log("All Pawns Placed. Your decision is now locked, prepare to face dire consequences.");

        if (playerConfirmedPlacement == false)  {
            OnPlayerConfirmPlacement?.Invoke();
            playerConfirmedPlacement = true;
            FindObjectOfType<SetupState>().GoToPlayer1State();
            FindObjectOfType<PlayerPlacementData>().Placed();
        }
    }
    public void AttackConfirmation() {
        Debug.Log("You have confirmed your attack location.");

            OnPlayerConfirmAttack?.Invoke();
            FindObjectOfType<PlayerActionState>().EndAttackConfirm();
            FindObjectOfType<SetupState>().GoToPlayer2State();
    }
}
