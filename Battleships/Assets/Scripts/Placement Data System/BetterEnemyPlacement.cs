using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterEnemyPlacement : MonoBehaviour {

    //an array of pawns
    public List<GameObject> pawnPrefabs; // this is the default pawn prefabs used to assign the player's army
    public List<GameObject> pawnsInBattle; // this will be the enemy's pawns

    [SerializeField] private GridManager enemyGridManager;
    [SerializeField] private bool enemiesShown = false;
    public bool allPawnsPlaced = false;
    private GameObject tile;

    private PawnOrientation pawnOrientation = PawnOrientation.HORIZONTAL;
    public GameObject initialCoords;

    private void Start() {
        CheckPawnList();
        PawnPlacement();
    }
    public void CheckPawnList() {
        if (pawnPrefabs.Count < 5) {
            Debug.Log("pawn prefab list (Enemy) must have 5 elements.");
            return;
        }
        else {
            ChooseRandomPawns(5);
        }
    }
    //TODO: spawn enemy prefabs, disactivate them so player cant see them
    //TODO: logic to make enemy prefabs be assigned to a place on a board
    /*
 
        Pete Planning for enemy placement: very simplified approach first
        "initialize" the pawns and pawns in the battle. (already done)
        get a pawn, choose an orientation and get its size
        instead of potentially placing it on a wrong place, we garantee its on a right place. IE: if horizontal and size 3, we try to place it anywhere on 7x7 grid
    */
    private void PawnPlacement() {
        Vector2 pos = new Vector2(-1,-1);

        for (int i = 0; i < pawnsInBattle.Count; i++) {
            int pawnSize = pawnsInBattle[i].GetComponent<Pawn>().GetPawnSize();
            AssignPawnOrientation();

            if (pawnSize == 1) {
                pos = GetRandomVector2(0, 10, 0, 10);
                Debug.Log("One pawn");
            }
            else if (pawnSize == 2) {
                pos = PawnPosition(pawnSize);
            }
            else if (pawnSize == 3) {
                pos = PawnPosition(pawnSize);
            }
            else if (pawnSize == 4) {
                pos = PawnPosition(pawnSize);
            }
            else if (pawnSize == 5) {
                pos = PawnPosition(pawnSize);
            } else {
                Debug.Log("Bro...");
            }
            PlacePawn(pos, pawnsInBattle[i]);
        }
    }
    private Vector2 PawnPosition(int pawnSize) { 
        Vector2 pos = new Vector2(-1, -1);

        do {
            if (pawnOrientation == PawnOrientation.HORIZONTAL) {
                pos = GetRandomVector2(0, (10 - pawnSize) + 1, 0, 10);
                Debug.Log("Hori: " + pos);
                return pos;
            }
            else { // Vertical
                pos = GetRandomVector2(0, 10, 0, (10 - pawnSize) + 1);
                Debug.Log("Vert: " + pos);
                return pos;
            }
            tile = enemyGridManager.GetTileAtPosition(pos);
        } while (tile.GetComponent<CubeVisual>().GetOccupied() == true);
    }
    private void PlacePawn(Vector2 pos, GameObject pawn) {
        pawn.GetComponent<PawnVisual>().ChangePawnVisual(pawnOrientation);
        pawn.transform.position = tile.GetComponent<CubeVisual>().GetCubeMidPosition();
    }
    private void ChooseRandomPawns(int numPawns) {
        int ranNum;
        for (int i = 0; i < numPawns; i++) {
            ranNum = Random.Range(0, 5)+1; // random number 1, 2, 3, 4, or 5
            GameObject pawn = Instantiate(PawnPrefabOfSize(ranNum), initialCoords.transform.position, Quaternion.identity);
            pawnsInBattle.Add(pawn);
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
    private void AssignPawnOrientation() {
        if (GetRandomNumber(1, 3) == 1) { // Vertical
            pawnOrientation = PawnOrientation.VERTICAL;
        }
        else { // Horizontal
            pawnOrientation = PawnOrientation.HORIZONTAL;
        }
    }  
    public int GetRandomNumber(int min, int maxExclusive) {
        return Random.Range(min, maxExclusive);
    }
    private Vector2 GetRandomVector2(int minX, int maxExclusiveX, int minY, int maxExclusiveY) {
        return new Vector2(Random.Range(minX, maxExclusiveX), Random.Range(minY, maxExclusiveY));
    }
}
