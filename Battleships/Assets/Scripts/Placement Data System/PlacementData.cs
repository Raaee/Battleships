using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This shows the status of the player/enemy placement positions of pawns 
/// </summary>
public class PlacementData : MonoBehaviour
{
    //an array of pawns
    public List<GameObject> pawnPrefabs; // this is the default pawn prefabs used to assign the player's army
    public List<GameObject> pawnsInBattle; // this will be the player's pawns

   //enum of player or enemy 
   [SerializeField] private Team team;

    private int ranNum;

    public bool allPawnsPlaced;
    [SerializeField] Button confirmButton;

    void Start() {
        CheckPawnList();
    }

    void Update() {
        ConfirmPlacement();
    }
    public void CheckPawnList() {
        if (pawnPrefabs.Count == 0) {
            Debug.Log("pawn prefab list is empty.");
            return;
        } else {
            ChooseRandomPawns(5);
            Debug.Log("choosing random pawns.");
        }
    }
    public void ChooseRandomPawns(int numPawns) {
        for (int i = 0; i < numPawns; i++) {
            ranNum = Random.Range(1, 6); // random number 1, 2, 3, 4, or 5
            pawnsInBattle.Add(PawnPrefabOfSize(ranNum));
          //  Debug.Log("Size: " + pawnsInBattle[i].GetPawnSize() + " / " + pawnsInBattle[i].name);
        }
    }
    private GameObject PawnPrefabOfSize(int size) {
        for (int i = 0; i < pawnPrefabs.Count; i++) {
            if (pawnPrefabs[i].GetComponent<Pawn>().GetPawnSize() == size) {
                return pawnPrefabs[i];
            }
        }
        return null;
    }
    public void GetPawnAmount() {
        
    }

    // checks if there is a boolean in the Pawn that is false:
    public void CheckPawnPlacement() {
        // ****** MUST CHANGE FOR LOOP VARIABLE AFTER ADDING PLAYER PAWNS IN THE SCENE *****
        foreach (GameObject p in pawnsInBattle) {
            if (p.GetComponent<Pawn>().GetPlacedStatus() == false) {
                allPawnsPlaced = false;
                return;
            }
        }
        if (pawnsInBattle.Count == 0) {
            allPawnsPlaced = false;
        }
        else {
            allPawnsPlaced = true;
        }        
    }
    // activates and deactivates the confirmation button depending on whether all pawns are placed:
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

