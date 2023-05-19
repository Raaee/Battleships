using System;
using UnityEngine;

public class PotentialShipPlacement : MonoBehaviour
{
    [HideInInspector] public CubeVisual currentHighlightedCubeVisual = null;
    [SerializeField] private GridManager gridManager;

    [SerializeField] private PawnOrientation pawnOrientation = PawnOrientation.VERTICAL;
    [SerializeField] [Range(1, 5)] private int sizeOfPawn = 1;

    private Pawn currentPawn;
    private void Update() {
        AssignPawnOrientation();
    }
    private void AssignPawnOrientation() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) { //forward
            pawnOrientation = PawnOrientation.VERTICAL;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) { //backwards
            pawnOrientation = PawnOrientation.HORIZONTAL;
        }
    }
    private void ShowPotentialShipPlacement() {

        Vector2 currTilePos = gridManager.GetPositionAtTile(currentHighlightedCubeVisual.gameObject);
        if (pawnOrientation == PawnOrientation.VERTICAL)
        {
            if (currTilePos.y + sizeOfPawn > 10)
            {
                RemoveAllHighlights();
                return;
            }

            //now we can highlight 
            HighlightPotentialShipPlacementVert((int)currTilePos.x, (int)currTilePos.y);
        }
        else //we want to do samething but horizontally 
        {
            if (currTilePos.x + sizeOfPawn > 10)
            {
                RemoveAllHighlights();
                return;
            }

            //now we can highlight horizontally 
            HighlightPotentialShipPlacementHori((int)currTilePos.x, (int)currTilePos.y);

        }
    }

    private void HighlightPotentialShipPlacementVert(int x, int y)
    {
        //start at current tile, and highlight the next tiles to the right for size of pawn times 
        for (int i = y; i < (y + sizeOfPawn); i++)
        {
            var tileGO = gridManager.GetTileAtPosition(new Vector2(x, i));
            if (tileGO)
                tileGO.GetComponent<CubeVisual>().ShowHighlight();
        }
    }

    private void HighlightPotentialShipPlacementHori(int x, int y)
    {
        for (int i = x; i < (x + sizeOfPawn); i++)
        {
            var tileGO = gridManager.GetTileAtPosition(new Vector2(i, y));

            if (tileGO)
                tileGO.GetComponent<CubeVisual>().ShowHighlight();
        }
    }


    public void AssignCurrentTileVisual(CubeVisual cubeVisual)
    {
        currentHighlightedCubeVisual = cubeVisual;
        ShowPotentialShipPlacement();
    }

    public void RemoveCurrentTileVisual()
    {
        currentHighlightedCubeVisual = null;
        RemoveAllHighlights();
    }

    private void RemoveAllHighlights()
    {
        foreach (var go in gridManager.GetTiles())
        {
            var tileColor = go.Value.gameObject.GetComponent<CubeVisual>();
            if (tileColor != null)
            {
                tileColor.HideHighlight();
            }
        }
    }
    public CubeVisual GetCurrentHighlightedCubeVisual() {
        return currentHighlightedCubeVisual;
    }
    public Vector3 GetCurrentHighlightedCenterPoint() {
        return currentHighlightedCubeVisual.GetCubeMidPosition();
    }
}

public enum PawnOrientation
{
    VERTICAL,
    HORIZONTAL
}