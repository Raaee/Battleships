using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotentialShipPlacement : MonoBehaviour
{
    private CubeVisual currentHighlightedCubeVisual = null;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PlayerPlacementData playerPlacementData;

    private PawnOrientation pawnOrientation = PawnOrientation.HORIZONTAL;
    private int sizeOfPawn = 1; // this changes when a pawn is selected

    [HideInInspector] public UnityEvent OnMouseScrolled;

    private List<GameObject> lastHighlightedGameObjects; //making a temp list, if pawn is placed then this is what is sent to the pawn data 

    private void Start()
    {
        lastHighlightedGameObjects = new List<GameObject>();
      //  GameObject.Instantiate(prefab, location, Quaternion.identity);
    }

    private void Update() {
        AssignPawnOrientation();
    }
    private void AssignPawnOrientation() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) { //forward
            pawnOrientation = PawnOrientation.VERTICAL;
            OnMouseScrolled.Invoke();
            ShowPotentialShipPlacement();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) { //backwards
            pawnOrientation = PawnOrientation.HORIZONTAL;
            OnMouseScrolled.Invoke();
            ShowPotentialShipPlacement();
        }
    }
    private void ShowPotentialShipPlacement()
    {
        RemoveAllHighlights();
        if (currentHighlightedCubeVisual == null) return;
        
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
        ResetLastHighlightedGameObjects();
        
        //start at current tile, and highlight the next tiles to the right for size of pawn times 
        for (int i = y; i < (y + sizeOfPawn); i++) {
            
            var tileGO = gridManager.GetTileAtPosition(new Vector2(x, i));
            
            if (tileGO)
            {
                if (CheckIfAlreadyPlaced(x, i))
                {

                    RemoveCurrentTileVisual();
                    RemoveAllHighlights();
                    return;
                }
                tileGO.GetComponent<CubeVisual>().ShowHighlight();
                lastHighlightedGameObjects.Add(tileGO);
            }              
        }
    }
    private void HighlightPotentialShipPlacementHori(int x, int y)
    {
        ResetLastHighlightedGameObjects();
        for (int i = x; i < (x + sizeOfPawn); i++)
        {
            var tileGO = gridManager.GetTileAtPosition(new Vector2(i, y));

            if (tileGO)
            {
                if (CheckIfAlreadyPlaced(i, y))
                {
                    RemoveCurrentTileVisual();
                    RemoveAllHighlights();
                    return;
                }
                lastHighlightedGameObjects.Add(tileGO);
                tileGO.GetComponent<CubeVisual>().ShowHighlight();
            }
                
        }
    }    
    private bool CheckIfAlreadyPlaced(int x, int y)
    {
        //this is the vector we are checking against
        Vector2 checkingVector2 = new Vector2(x, y);
        
        //go through all pawns in battle
        foreach (GameObject pawnGO in playerPlacementData.pawnsInBattle)
        {
           List<Vector2> pawnCoordinates =  pawnGO.GetComponent<Pawn>().pawnCoords;
           if (pawnCoordinates.Count <= 0) continue; 
           
           //go through their pawn coordinates
           foreach (Vector2 pawnCoord in pawnCoordinates)
           {
               //if one of their pawn coordinate matches with the checking vector2, then return true cuz that means its already placed
               if (pawnCoord.Equals(checkingVector2))
               {
                 
                   return true;
               }
           }
        }
        return false;
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

    private void ResetLastHighlightedGameObjects()
    {
        lastHighlightedGameObjects = new List<GameObject>();
    }

    public List<GameObject> GetLastHighlightedObjects()
    {
        if (lastHighlightedGameObjects.Count == 0) return null;

        return lastHighlightedGameObjects;
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
    
    public PawnOrientation GetPawnOrientation() {
        return pawnOrientation;
    }
    
    public void SetPawnSize(int size) {
        sizeOfPawn = size;
    }
}

public enum PawnOrientation
{
    VERTICAL,
    HORIZONTAL
}