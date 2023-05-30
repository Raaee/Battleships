using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This shows the status of the player/enemy placement positions of pawns 
/// </summary>
public class PlacementData : MonoBehaviour
{
   //an array of pawns
   public List<Pawn> pawns;
   
   //enum of player or enemy 
   [SerializeField] private Team team;

    private bool allPawnsPlaced;
    [SerializeField] Button confirmButton;

    void Update() {
        ConfirmPlacement();
    }

    public void CheckPawnPlacement() {
        foreach (Pawn p in pawns) {
            if (p.GetPlacedStatus() == false) {
                allPawnsPlaced = false;
                return;
            }
        }
        allPawnsPlaced = true;
    }
    public void ConfirmPlacement() {
        CheckPawnPlacement();
        if (allPawnsPlaced) {
            confirmButton.gameObject.SetActive(true);
        } else {
            confirmButton.gameObject.SetActive(false);
        }
    }
}

public enum Team {
   NONE,
   PLAYER,
   ENEMY
}

