using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlacementData : MonoBehaviour
{
    //an array of pawns
    public List<GameObject> pawnPrefabs; // this is the default pawn prefabs used to assign the player's army
    public List<GameObject> pawnsInBattle; // this will be the enemy's pawns

    [SerializeField] private GridManager enemyGridManager;
  // [SerializeField] private bool enemiesShown = false;
    public bool allPawnsPlaced = false;
    private bool vertUpValid, vertDownValid, horiLeftValid, horiRightValid;

    private PawnOrientation pawnOrientation;
    public GameObject initialCoords;

    private void Start() {
        CheckPawnList();
        CheckPosition();
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
    private void Update() {
        //ShowEnemiesOnBoard(enemiesShown);
    }
    //TODO: spawn enemy prefabs, disactivate them so player cant see them
    private void ShowEnemiesOnBoard(bool b) {
        foreach (GameObject p in pawnsInBattle) {
            p.SetActive(b);
        }
    }
    private void ChooseRandomPawns(int numPawns) {
        int ranNum;
        for (int i = 0; i < numPawns; i++) {
            ranNum = Random.Range(1, 6); // random number 1, 2, 3, 4, or 5
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
    private void PlacePawn(GameObject pawnGO, PawnOrientation orientation, int direction, Vector2 selectedPos) {

        // move pawn to location
        // Set pawn coords to Pawn class
        // also set CubeVisual.occupied = true for each cube the pawn occupies
        int pawnSize = pawnGO.GetComponent<Pawn>().GetPawnSize();
        List<Vector2> pawnCoords = new List<Vector2>();
        Pawn pawn = pawnGO.GetComponent<Pawn>();
        GameObject tile = null;

        if (orientation == PawnOrientation.VERTICAL) {
            if (direction == 1) {
                // vertical placement upward
                Debug.Log("placed vert up");
                for (int i = (int) selectedPos.x; i < (selectedPos.x + pawnSize); i++) {
                    tile = enemyGridManager.GetTileAtPosition(new Vector2(i, selectedPos.y));
                    tile.GetComponent<CubeVisual>().SetOccupied(true);
                    pawnCoords.Add(new Vector2(i, selectedPos.y));
                }
                SetPawnCoordsAndVisual(pawn, pawnCoords, PawnOrientation.VERTICAL, pawnGO, tile);
            } else {
                // vertical placement downward
                Debug.Log("placed vert down");
                for (int i = (int)selectedPos.x; i > (selectedPos.x - pawnSize); i--) {
                    tile = enemyGridManager.GetTileAtPosition(new Vector2(i, selectedPos.y));
                    tile.GetComponent<CubeVisual>().SetOccupied(true);
                    pawnCoords.Add(new Vector2(i, selectedPos.y));
                }
                SetPawnCoordsAndVisual(pawn, pawnCoords, PawnOrientation.VERTICAL, pawnGO, tile);
            }
        } else {
            if (direction == 1) {
                // horizontal placement left
                Debug.Log("placed hori left");
                for (int i = (int)selectedPos.y; i < (selectedPos.y + pawnSize); i++) {
                    tile = enemyGridManager.GetTileAtPosition(new Vector2(selectedPos.x, i));
                    tile.GetComponent<CubeVisual>().SetOccupied(true);
                    pawnCoords.Add(new Vector2(selectedPos.x, i));
                }
                SetPawnCoordsAndVisual(pawn, pawnCoords, PawnOrientation.HORIZONTAL, pawnGO, tile);
            }
            else {
                // horizontal placement right
                Debug.Log("placed hori right");
                for (int i = (int)selectedPos.y; i > (selectedPos.y - pawnSize); i--) {
                    tile = enemyGridManager.GetTileAtPosition(new Vector2(selectedPos.x, i));
                    tile.GetComponent<CubeVisual>().SetOccupied(true);
                    pawnCoords.Add(new Vector2(selectedPos.x, i));
                }
                SetPawnCoordsAndVisual(pawn, pawnCoords, PawnOrientation.HORIZONTAL, pawnGO, tile);
            }
        }
    }    
    private void SetPawnCoordsAndVisual(Pawn pawn, List<Vector2> pawnCoords, PawnOrientation orientation, GameObject pawnGO, GameObject tile) {
        pawn.SetPawnCoordinates(pawnCoords);
        pawn.GetComponent<PawnVisual>().ChangePawnVisual(orientation);
        pawnGO.transform.position = tile.GetComponent<CubeVisual>().GetCubeMidPosition();
    }
    //TODO: logic to make enemy prefabs be assigned to a place on a board
    private void CheckPosition() {

        for (int i = 0; i < pawnsInBattle.Count; i++) {
            ResetDirections();
            AssignPawnOrientation();
            Vector2 randomV2 = GetRandomVector2(); // initial spot
            int pawnSize = pawnsInBattle[i].GetComponent<Pawn>().GetPawnSize();

            if (pawnOrientation == PawnOrientation.VERTICAL) { // Vertical
                CheckVerticalPos(randomV2, pawnSize);
                if (vertUpValid == false && vertDownValid == false) {
                    i--;
                    Debug.Log("Vert ********* Getting another v2");
                } 
                else if (vertUpValid && vertDownValid == false) {
                    PlacePawn(pawnsInBattle[i], pawnOrientation, 1, randomV2);
                } 
                else if (vertDownValid && vertUpValid == false) {
                    PlacePawn(pawnsInBattle[i], pawnOrientation, 2, randomV2);
                } 
                else if (vertUpValid && vertDownValid) {
                    PlacePawn(pawnsInBattle[i], pawnOrientation, GetRandomNumber(1,3), randomV2);
                }
            }
            else { // Horizontal
                CheckHorizontalPos(randomV2, pawnSize);
                if (horiLeftValid == false && horiRightValid == false) {
                    i--;
                    Debug.Log("Hori ********* Getting another v2");
                }
                else if (horiLeftValid && horiRightValid == false) {
                    PlacePawn(pawnsInBattle[i], pawnOrientation, 1, randomV2);
                }
                else if (horiRightValid && horiLeftValid == false) {
                    PlacePawn(pawnsInBattle[i], pawnOrientation, 2, randomV2);
                }
                else if (horiLeftValid && horiRightValid) {
                    PlacePawn(pawnsInBattle[i], pawnOrientation, GetRandomNumber(1, 3), randomV2);
                }
            }
        }
    }
    private void ResetDirections() {
        vertDownValid = false;
        vertUpValid = false;
        horiLeftValid = false;
        horiRightValid = false;
    }
    // [row][col] = [x][y]
    // vertical changes x/row
    // horizontal changes y/col
    private void CheckVerticalPos(Vector2 randomV2, int pawnSize) {
        Debug.Log("Vert: " + randomV2 + " / Pawn Size: " + pawnSize);

        for (int n = (int)randomV2.x; n < ((int)randomV2.x + pawnSize); n++) { // checking every position ABOVE the initial coordinate

            GameObject tileGO = enemyGridManager.GetTileAtPosition(new Vector2(n, randomV2.y));
           
            if ((int)randomV2.x + pawnSize > 10) {
                vertUpValid = false;
                return;
            }
            if (tileGO.GetComponent<CubeVisual>().GetOccupied() == false) {
                vertUpValid = true;
            }
            else {
                vertUpValid = false;
                return;
            }
        }
        for (int n = (int)randomV2.x; n < ((int)randomV2.x - pawnSize); n--) { // checking every position BELOW the initial coordinate

            GameObject tileGO = enemyGridManager.GetTileAtPosition(new Vector2(n, randomV2.y));

            if ((int)randomV2.x - pawnSize < 0) {
                vertDownValid = false;
                return;
            }
            if (tileGO.GetComponent<CubeVisual>().GetOccupied() == false) {
                vertDownValid = true;
            }
            else {
                vertDownValid = false;
                return;
            }
        }
    }
    private void CheckHorizontalPos(Vector2 randomV2, int pawnSize) {
        Debug.Log("Hori: " + randomV2 + " / Pawn Size: " + pawnSize);

        for (int n = (int)randomV2.y; n < ((int)randomV2.y + pawnSize); n++) { // checking every position RIGHT of the initial coordinate

            GameObject tileGO = enemyGridManager.GetTileAtPosition(new Vector2(randomV2.x, n));

            if ((int)randomV2.y + pawnSize > 10) {
                horiRightValid = false;
                return;
            }
            if (tileGO.GetComponent<CubeVisual>().GetOccupied() == false) {
                horiRightValid = true;
            }
            else {
                horiRightValid = false;
                return;
            }
        }
        for (int n = (int)randomV2.y; n < ((int)randomV2.y - pawnSize); n--) { // checking every position LEFT of the initial coordinate

            GameObject tileGO = enemyGridManager.GetTileAtPosition(new Vector2(randomV2.x, n));

            if ((int)randomV2.y - pawnSize < 0) {
                horiLeftValid = false;
                return;
            }
            if (tileGO.GetComponent<CubeVisual>().GetOccupied() == false) {
                horiLeftValid = true;
            }
            else {
                horiLeftValid = false;
                return;
            }
        }
    }
    public int GetRandomNumber(int min, int maxExclusive) {
        return Random.Range(min, maxExclusive);
    }
    private Vector2 GetRandomVector2() {
        return new Vector2(Random.Range(0, 10), Random.Range(0, 10));
    }

}
