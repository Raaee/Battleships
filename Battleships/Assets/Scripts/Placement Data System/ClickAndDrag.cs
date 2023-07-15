using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// The dragging script for the pawns.
/// </summary>
public class ClickAndDrag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    private PotentialShipPlacement potentialShipPlacement;
    private Pawn currentPawn;
    private AmountOfPlayerPawnsUI playerPawnsUI;

    public UnityEvent OnPawnPlaced;

    private bool IsActive = true;
    private ButtonFunctions buttonsFunctions;

    private Vector3 originalSpawnLocation;

    private CubeVisual currentCV;
    private Vector3 lastChosenLoc;
    private InputData inputData;

    private void Awake()
    {
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        currentPawn = GetComponent<Pawn>();
        buttonsFunctions = FindObjectOfType<ButtonFunctions>(); 
        buttonsFunctions.OnPlayerConfirmPlacement.AddListener(DisableSelf);
        playerPawnsUI = FindObjectOfType<AmountOfPlayerPawnsUI>();
        inputData = FindObjectOfType<InputData>();

    }
    private void Start()
    {
        originalSpawnLocation = transform.position;
    }

    public void DisableSelf()
    {
        IsActive = false;
        
    }


    private void Update() {
        if(!IsActive) return;
        
        if (dragging) {
         //   potentialShipPlacement.AssignPawnOrientation();
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPos;
            if(inputData.GetCubeVisual() != null)  currentPawn.transform.position = inputData.GetCubeVisual().GetCubeMidPosition();
        }
    }

    private void OnMouseDown() {
        if(!IsActive) return;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        //changing the highlightedd size for the potential ship placement 
     //   potentialShipPlacement.SetPawnSize(currentPawn.GetPawnSize());
    }
    private void OnMouseUp() {   
        if(!IsActive) return;
        dragging = false;
        SnapToCube();
    }
    private void SnapToCube() {
        if (inputData.GetCubeVisual() != null)
        {
            if (potentialShipPlacement.GetIsPawnOverPawn())
            {
                currentPawn.SetPlacedStatus(false);
                ResetToOriginalSpawnPosition();
                return;
            }
            lastChosenLoc = currentPawn.transform.position;
            currentPawn.transform.position =  inputData.GetCubeVisual().GetCubeMidPosition();
            currentPawn.SetPlacedStatus(true);
            OnPawnPlaced?.Invoke();
            playerPawnsUI.UpdateUI(currentPawn.GetPawnSize());
            currentPawn.SetPawnCoordinates();
            // gotta fix this. doesnt remove occupy when changing pawn position. only changes when dropping out of grid
            OccupyCubes(true);
        }
        else
        {
          //  Debug.Log("you dropped the pawn but you werent over a cube. so bad.");
            currentPawn.SetPlacedStatus(false);
            ResetToOriginalSpawnPosition();
            OccupyCubes(false);
        }
        potentialShipPlacement.RemoveCurrentTileVisual();
    }
    private void OccupyCubes(bool occupied) {
        List<GameObject> lastHighlightedGameObjects = potentialShipPlacement.GetLastHighlightedCubes();
/*
        for (int i = 0; i < currentPawn.GetPawnSize(); i++) {
            if (occupied)
                lastHighlightedGameObjects[i].GetComponent<CubeVisual>().ChangeMaterialOnHitState(CubeHitState.OCCUPIED);
            else
                lastHighlightedGameObjects[i].GetComponent<CubeVisual>().ChangeMaterialOnHitState(CubeHitState.NONE);
        }
*/
    }

    private void ResetToOriginalSpawnPosition()
    {
        transform.position = originalSpawnLocation;
    }
    public bool GetIsDragging()
    {
        return dragging;
    }

   
}
