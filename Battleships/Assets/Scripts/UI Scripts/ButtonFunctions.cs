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
        
            OnPlayerConfirmPlacement?.Invoke(); //use to disable the click and drags, PLaced method in Playerplacementdata, remove the UI numbers, moving to thet player state
            placementConfirmedButton.SetActive(false);
            Destroy(placementConfirmedButton);
        
    }
    // This method is attached to the attack button:
    public void AttackConfirmation() {
        
        OnPlayerConfirmAttack?.Invoke(); //it will confirm attack in player action state, end the attack confirm, do feeddback, and then go to the cpu (2) player sttate 
    }
}
