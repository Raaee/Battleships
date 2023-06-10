using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterEnemyPlacement : MonoBehaviour {

    //an array of pawns
    public List<GameObject> pawnPrefabs; // this is the default pawn prefabs used to assign the player's army
    public List<GameObject> pawnsInBattle; // this will be the enemy's pawns

    [SerializeField] private GridManager enemyGridManager;
    [SerializeField] private bool enemiesShown = false;  // this is for hiding the pawns on the battlefield
    private GameObject tile = null;

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

    // assigns orientation and calls all other placement and checking methods:
    private void PawnPlacement() {
        Vector2 pos = new Vector2(-1,-1);

        for (int i = 0; i < pawnsInBattle.Count; i++) {
            int pawnSize = pawnsInBattle[i].GetComponent<Pawn>().GetPawnSize();
            AssignPawnOrientation();

            switch(pawnSize) {
                case 1:
                    pos = GetRandomVector2(0, 10, 0, 10);
                    Debug.Log("One pawn");
                    break;
                case 2: pos = PawnPosition(pawnSize);
                    break;
                case 3: pos = PawnPosition(pawnSize);
                    break;
                case 4: pos = PawnPosition(pawnSize);
                    break;
                case 5: pos = PawnPosition(pawnSize);
                    break;
            }
            PlacePawn(pos, pawnsInBattle[i]);
        }
    }
    // chooses random vector position for pawn placement AND calls CheckOccupy. returns the valid position:
    private Vector2 PawnPosition(int pawnSize) { 
        Vector2 pos = new Vector2(-1, -1);
        bool validPos;

        do {
            validPos = true;
            if (pawnOrientation == PawnOrientation.HORIZONTAL) {
                pos = GetRandomVector2(0, (10 - pawnSize) + 1, 0, 10);
                Debug.Log("Hori: " + pos + " / Size: " + pawnSize);

                validPos = CheckOccupy(pos.x, pos.y, pawnSize);
            }
            else { // Vertical
                pos = GetRandomVector2(0, 10, 0, (10 - pawnSize) + 1);
                Debug.Log("Vert: " + pos + " / Size: " + pawnSize);

                validPos = CheckOccupy(pos.y, pos.x, pawnSize);
            }          
        } while (!validPos);

        return pos;
    }
    // checks if all tiles within the given pawn place location are occupied or not. returns valid:
    private bool CheckOccupy(float n, float o, int pawnSize) {
        // n is the variable that changes, o is the variable that stays the same
        
        // The correct way the coords should work:
        // [row][col] = [x][y]
        // vertical changes x/row
        // horizontal changes y/col

        tile = null;

        for (int i = (int)n; i < n + pawnSize; i++) {
            if (pawnOrientation == PawnOrientation.HORIZONTAL) {
                tile = enemyGridManager.GetTileAtPosition(new Vector2(o, i));
                Debug.Log("**** Hori: " + tile);
            } else { // Vertical
                tile = enemyGridManager.GetTileAtPosition(new Vector2(i, o));
                Debug.Log("**** Vert: " + tile);
            }
            
            if (tile.GetComponent<CubeVisual>().GetOccupied() == true) {
                Debug.Log("------- No work: " + new Vector2(n, o) + " ----------");
                return false;
            }
        }
        return true;        
    }
    // method that calls OccupyCoords AND places the pawns in the correct spots:
    private void PlacePawn(Vector2 pos, GameObject pawn) {
        pawn.GetComponent<PawnVisual>().ChangePawnVisual(pawnOrientation);
        
        if (pawnOrientation == PawnOrientation.HORIZONTAL) {
            OccupyCoords(pos.x, pos.y, pawn.GetComponent<Pawn>().GetPawnSize(), pawn);
        } else {
            OccupyCoords(pos.y, pos.x, pawn.GetComponent<Pawn>().GetPawnSize(), pawn);
        }

        tile = enemyGridManager.GetTileAtPosition(pos);
        pawn.transform.position = tile.GetComponent<CubeVisual>().GetCubeMidPosition();
    }
    // sets occupy variable of tile = true AND assigns the pawns' coords to their list:
    private void OccupyCoords(float n, float o, int pawnSize, GameObject pawn) {
        List<Vector2> pCoords = new List<Vector2>();

        for (int i = (int)n; i < n + pawnSize; i++) {
            if (pawnOrientation == PawnOrientation.HORIZONTAL) {
                tile = enemyGridManager.GetTileAtPosition(new Vector2(o, i));
            }
            else { // Vertical
                tile = enemyGridManager.GetTileAtPosition(new Vector2(i, o));
            }

            tile.GetComponent<CubeVisual>().SetOccupied(true);
            pCoords.Add(enemyGridManager.GetPositionAtTile(tile));
        }
        pawn.GetComponent<Pawn>().SetPawnCoordinates(pCoords);
    }
    // chooses random pawns to go to battle based on their size:
    private void ChooseRandomPawns(int numPawns) {
        int ranNum;
        for (int i = 0; i < numPawns; i++) {
            ranNum = Random.Range(0, 5)+1; // random number 1, 2, 3, 4, or 5
            GameObject pawn = Instantiate(PawnPrefabOfSize(ranNum), initialCoords.transform.position, Quaternion.identity);
            pawnsInBattle.Add(pawn);
        }
    }
    // Returns the pawn gameobject according to the size you pass it:
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
