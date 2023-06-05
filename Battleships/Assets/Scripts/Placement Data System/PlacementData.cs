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
    
  /*  [SerializeField] GameObject initialCoords, pawnSpawn1, pawnSpawn2, pawnSpawn3, pawnSpawn4, pawnSpawn5;*/
    public List<GameObject> pawnSpawnLocations;
    
    private int ranNum;
    private bool allPawnsPlaced;
    [SerializeField] Button confirmButton;

    void Start() {
        CheckPawnList();
    }

    void Update() {
        ConfirmPlacement();
    }
    
    public void CheckPawnList() {
        if (pawnPrefabs.Count < 5) {
            Debug.Log("pawn prefab list must have 5 elements.");
            return;
        } else {
            //ChooseRandomPawns(5);
            Pete_SpawnInitialPawns(5);
        }
    }

    private void Pete_SpawnInitialPawns(int amountOfPawns)
    {
        for (int i = 0; i < amountOfPawns; i++)
        {
            ranNum = Random.Range(0, 5); 
            GameObject pawn = Instantiate(pawnPrefabs[ranNum], pawnSpawnLocations[i].transform.position, Quaternion.identity);
            pawnsInBattle.Add(pawn);
        }
        
    }
    /*
    public void ChooseRandomPawns(int numPawns) {
        for (int i = 0; i < numPawns; i++) {
            ranNum = Random.Range(1, 6); // random number 1, 2, 3, 4, or 5
            var pawn = Instantiate(PawnPrefabOfSize(ranNum), initialCoords.transform.position, Quaternion.identity);
            pawnsInBattle.Add(pawn);
        }
        SpawnInitialPawns();
    }
    */
    
    
    private GameObject PawnPrefabOfSize(int size) {
        for (int i = 0; i < pawnPrefabs.Count; i++) {
            if (pawnPrefabs[i].GetComponent<Pawn>().GetPawnSize() == size) {
                return pawnPrefabs[i];
            }
        }
        return null;
    }
    // checks if there is a false place status in each pawn:
    public void CheckPawnPlacement() {
        foreach (GameObject p in pawnsInBattle) {
            if (p.GetComponent<Pawn>().GetPlacedStatus() == false) {
                allPawnsPlaced = false;
                return;
            }
        }
        if (pawnsInBattle.Count == 0) {
            Debug.Log("Nu UH");
            allPawnsPlaced = false;
        }
        else {
            Debug.Log("all placed: " + allPawnsPlaced);
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
    
    /*
    public void SpawnInitialPawns() {
        foreach (GameObject p in pawnsInBattle) {
            switch (p.GetComponent<Pawn>().GetPawnSize()) {
                case 1:
                    p.transform.position = pawnSpawn1.transform.position;
                    break;
                case 2:
                    p.transform.position = pawnSpawn2.transform.position;
                    break;
                case 3:
                    p.transform.position = pawnSpawn3.transform.position;
                    break;
                case 4:
                    p.transform.position = pawnSpawn4.transform.position;
                    break;
                case 5:
                    p.transform.position = pawnSpawn5.transform.position;
                    break;
            }
        }
    }
    */
}

public enum Team {
   NONE,
   PLAYER,
   ENEMY
}

