using System;
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
    

    [SerializeField] private GameObject placementConfirmedButton;
    [SerializeField] private PlayerPlacementData playerPlacementData;

    private void Awake()
    {
        placementConfirmedButton.SetActive(false);
    }

    private void Update()
    {
        if(placementConfirmedButton != null)
            placementConfirmedButton.SetActive(playerPlacementData.GetIsAllPawnsPlaced());

    }

    public void PlacementConfirmation() {
            Debug.Log("All Pawns Placed. Your decision is now locked, prepare to face dire consequences.");
        
            OnPlayerConfirmPlacement?.Invoke(); //use to disable the click and drags, PLaced method in Playerplacementdata, and moving to thet player state
            placementConfirmedButton.SetActive(false);
            Destroy(placementConfirmedButton);
        
    }
    public void AttackConfirmation() {
        Debug.Log("You have confirmed your attack location.");

            OnPlayerConfirmAttack?.Invoke();
            FindObjectOfType<PlayerActionState>().EndAttackConfirm();
            FindObjectOfType<SetupState>().GoToPlayer2State();
    }
}
