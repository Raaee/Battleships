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

    private float lerpSpeed = 18f;


    GameplayUIAudio gameplayUIAudio;
    private void Awake()
    {
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        currentPawn = GetComponent<Pawn>();
        buttonsFunctions = FindObjectOfType<ButtonFunctions>(); 
        buttonsFunctions.OnPlayerConfirmPlacement.AddListener(DeactivateDragging);
        playerPawnsUI = FindObjectOfType<AmountOfPlayerPawnsUI>();
        inputData = FindObjectOfType<InputData>();
        gameplayUIAudio = FindObjectOfType<GameplayUIAudio>();
        

    }
    private void Start()
    {
        originalSpawnLocation = transform.position;
    }

    public void DeactivateDragging()
    {
        IsActive = false;
        
    }


    private void Update() {
        if(!IsActive) return;
        
        if (dragging) {
        
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            //transform.position = newPos;

            transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);

            if (inputData.GetCubeVisual() != null)
            {             
                currentPawn.transform.position = inputData.GetCubeVisual().GetCubeMidPosition();
            }
        }
    }

    private void OnMouseDown() {
        if(!IsActive) return;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        currentPawn.ResetPawnCoords();
        //changing the highlightedd size for the potential ship placement 
        gameplayUIAudio.PickUpPawn();

    }
    private void OnMouseUp() {   
        if(!IsActive) return;
        dragging = false;
        SnapToCube();
    }
    private void SnapToCube() {
        if (inputData.GetCubeVisual() != null)
        {
            //check if the cube is part of the player cube
            if (inputData.GetPlayerGridManager().GetPositionAtTile(inputData.GetCubeVisual().gameObject).x < 0 )
            {                       
                ResetToOriginalSpawnPosition();
                return;
            }
     

            if (potentialShipPlacement.GetIsPawnOverPawn()) 
            {
                Debug.Log("theres already pawn here");            
                ResetToOriginalSpawnPosition();
                return;
            }


            MovePawn();

            if(!currentPawn.SetPawnCoordinates())
            {
                ResetToOriginalSpawnPosition();
                return;
            }


            // gotta fix this. doesnt remove occupy when changing pawn position. only changes when dropping out of grid
            OccupyCubes(true);
        }
        else
        {
            Debug.Log("you dropped the pawn but you werent over a cube. so bad.");
            
            ResetToOriginalSpawnPosition();
            OccupyCubes(false);
        }
        potentialShipPlacement.RemoveCurrentTileVisual();
    }

    private void MovePawn()
    {
        lastChosenLoc = currentPawn.transform.position;
        currentPawn.transform.position = Vector3.Lerp(currentPawn.transform.position, inputData.GetCubeVisual().GetCubeMidPosition(), lerpSpeed * Time.deltaTime);
        currentPawn.SetPlacedStatus(true);
        OnPawnPlaced?.Invoke();
        playerPawnsUI.UpdateUI(currentPawn.GetPawnSize());
        gameplayUIAudio.PlacedPawned();
    }

    private void OccupyCubes(bool occupied) {
        List<GameObject> lastHighlightedGameObjects = potentialShipPlacement.GetLastHighlightedCubes();
    }

    private void ResetToOriginalSpawnPosition()
    {
        currentPawn.SetPlacedStatus(false);
        transform.position = originalSpawnLocation;
    }
    public bool GetIsDragging()
    {
        return dragging;
    }

   
}
