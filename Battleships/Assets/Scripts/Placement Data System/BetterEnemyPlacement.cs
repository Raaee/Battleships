using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// The official enemy placement spawn system
/// </summary>
public class BetterEnemyPlacement : MonoBehaviour {

    //an array of pawns
    public List<GameObject> pawnPrefabs; // this is the default pawn prefabs used to assign the player's army
    public List<GameObject> pawnsInBattle; // this will be the enemy's pawns

    [SerializeField] private GridManager enemyGridManager;
   
    private GameObject tile = null;

    private PawnOrientation pawnOrientation = PawnOrientation.HORIZONTAL;
    public GameObject initialCoords;
    [SerializeField] private int numPawnsInBattle = 5;

    public void StartPlacement() {
        CheckPawnList();
        PawnPlacement();
    }
    public void CheckPawnList() {
        if (pawnPrefabs.Count < 4) {
            Debug.Log("pawn prefab list (Enemy) must have 4 elements.");
            return;
        }
        else {
            ChooseRandomPawns(numPawnsInBattle);
        }
    }
   
    // assigns orientation and calls all other placement and checking methods:
    private void PawnPlacement() {
        Vector2 pos = new Vector2(-1,-1);

        for (int i = 0; i < pawnsInBattle.Count; i++) {
            int pawnSize = pawnsInBattle[i].GetComponent<Pawn>().GetPawnSize();
            AssignPawnOrientation();

            switch(pawnSize) {
                case 1:
                    pos = GetRandomVector2(0, 10, 0, 10);
                    break;
                case 2: pos = PawnPosition(pawnSize);
                    break;
                case 3: pos = PawnPosition(pawnSize);
                    break;
                case 4: pos = PawnPosition(pawnSize);
                    break;
            }
            PlacePawn(pos, pawnsInBattle[i]);
            
            //disable pawns 
            pawnsInBattle[i].gameObject.GetComponent<ClickAndDrag>().DisableSelf();
            pawnsInBattle[i].gameObject.SetActive(false);
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
                //Debug.Log("Hori: " + pos + " / Size: " + pawnSize);

                validPos = CheckIfOccupy(pos.x, pos.y, pawnSize);
            }
            else { // Vertical
                pos = GetRandomVector2(0, 10, 0, (10 - pawnSize) + 1);
               // Debug.Log("Vert: " + pos + " / Size: " + pawnSize);

                validPos = CheckIfOccupy(pos.y, pos.x, pawnSize);
            }          
        } while (!validPos);

        return pos;
    }
    // checks if all tiles within the given pawn place location are occupied or not. returns valid:
    private bool CheckIfOccupy(float n, float o, int pawnSize) {
        // n is the variable that changes, o is the variable that stays the same
        
        // The correct way the coords should work:
        // [row][col] = [x][y]
        // vertical changes x/row
        // horizontal changes y/col

        tile = null;

        for (int i = (int)n; i < n + pawnSize; i++) {
            if (pawnOrientation == PawnOrientation.HORIZONTAL) {
                tile = enemyGridManager.GetTileAtPosition(new Vector2(o, i));
                //Debug.Log("**** Hori: " + tile);
            } else { // Vertical
                tile = enemyGridManager.GetTileAtPosition(new Vector2(i, o));
                //Debug.Log("**** Vert: " + tile);
            }
            
            if (tile.GetComponent<CubeVisual>().GetIsOccupied() == true) {
              //  Debug.Log("------- No work: " + new Vector2(n, o) + " ----------");
                return false;
            }
        }
        return true;        
    }
    // method that calls OccupyCoords AND places the pawns in the correct spots:
    private void PlacePawn(Vector2 pos, GameObject pawn) {
        pawn.GetComponent<PawnVisual>().ChangePawnVisual(pawnOrientation);
        
        if (pawnOrientation == PawnOrientation.HORIZONTAL) {
            SetOccupyCoords(pos.x, pos.y, pawn.GetComponent<Pawn>().GetPawnSize(), pawn);
        } else {
            SetOccupyCoords(pos.y, pos.x, pawn.GetComponent<Pawn>().GetPawnSize(), pawn);
        }

        tile = enemyGridManager.GetTileAtPosition(pos);
        pawn.transform.position = tile.GetComponent<CubeVisual>().GetCubeMidPosition();
    }
    // sets occupy variable of tile = true AND assigns the pawns' coords to their list:
    private void SetOccupyCoords(float n, float o, int pawnSize, GameObject pawn) {
        List<Vector2> pCoords = new List<Vector2>();

        for (int i = (int)n; i < n + pawnSize; i++) {
            if (pawnOrientation == PawnOrientation.HORIZONTAL) {
                tile = enemyGridManager.GetTileAtPosition(new Vector2(o, i));
            }
            else { // Vertical
                tile = enemyGridManager.GetTileAtPosition(new Vector2(i, o));
            }

            tile.GetComponent<CubeVisual>().SetIsOccupied(true);
            pCoords.Add(enemyGridManager.GetPositionAtTile(tile));
        }
        pawn.GetComponent<Pawn>().SetPawnCoordinates(pCoords);
    }
    // chooses random pawns to go to battle based on their size:
    private void ChooseRandomPawns(int numPawns) {
        int ranNum;
        for (int i = 0; i < numPawns; i++) {
            ranNum = Random.Range(0, 4)+1; // random number 1, 2, 3, or 4
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
    public Vector2 GetRandomVector2(int minX, int maxExclusiveX, int minY, int maxExclusiveY) {
        return new Vector2(Random.Range(minX, maxExclusiveX), Random.Range(minY, maxExclusiveY));
    }
    public bool CheckIfHit(Vector2 attackLoc) {
        bool hit = false;
        Vector2 correctLoc = new Vector2(attackLoc.y, attackLoc.x);

        for (int i = 0; i < pawnsInBattle.Count; i++) {
            Pawn pawn = pawnsInBattle[i].GetComponent<Pawn>();
            for (int n = 0; n < pawn.pawnCoords.Count; n++) {
                Vector2 pawnCoord = pawn.pawnCoords[n];
                if (pawnCoord == correctLoc) {
                    hit = true;
                }
            }
        }
        if (hit)
            return true;
        else
            return false;
    }
}
