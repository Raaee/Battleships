using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonFunctions : MonoBehaviour { 
    public UnityEvent OnPlayerConfirmPlacement;
    public UnityEvent OnPlayerConfirmAttack;
    private bool playerConfirmedPlacement = false;
    private bool playerConfirmedAttack = false;


    public void PlacementConfirmation() {
        Debug.Log("All Pawns Placed. Your decision is now locked, prepare to face dire consequences.");

        if (playerConfirmedPlacement == false)  {
            OnPlayerConfirmPlacement?.Invoke();
            playerConfirmedPlacement = true;
            FindObjectOfType<SetupState>().GoToPlayer1State();
            FindObjectOfType<PlacementData>().Placed();
        }
    }
    public void AttackConfirmation() {
        Debug.Log("You have confirmed your attack location.");

        if (playerConfirmedAttack == false) {
            OnPlayerConfirmAttack?.Invoke();
            playerConfirmedAttack = true;
            FindObjectOfType<Player1ActionState>().EndAttackConfirm();
            FindObjectOfType<SetupState>().GoToPlayer2State();
        }
    }
}
