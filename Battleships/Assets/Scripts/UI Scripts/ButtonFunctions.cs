using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonFunctions : MonoBehaviour
{

    public UnityEvent OnPlayerConfirmPlacement;
    private bool playerConfirmedPlacement = false;
    public void PlacementConfirmation() {
        Debug.Log("All Pawns Placed. Your decision is now locked, prepare to face dire consequences.");

        if (playerConfirmedPlacement == false)
        {
            OnPlayerConfirmPlacement?.Invoke();
            playerConfirmedPlacement = true;
            FindObjectOfType<SetupState>().GoToPlayer1State();
        }

    }
}
