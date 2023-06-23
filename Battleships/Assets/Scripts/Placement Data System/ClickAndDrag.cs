using System;
using UnityEngine;
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

    private void Awake()
    {
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        currentPawn = GetComponent<Pawn>();
        buttonsFunctions = FindObjectOfType<ButtonFunctions>(); 
        buttonsFunctions.OnPlayerConfirmPlacement.AddListener(DisableSelf);
        playerPawnsUI = FindObjectOfType<AmountOfPlayerPawnsUI>(); 
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
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPos;
            if(potentialShipPlacement.GetCurrentHighlightedCubeVisual() != null)  currentPawn.transform.position = potentialShipPlacement.GetCurrentHighlightedCubeVisual().GetCubeMidPosition();
        }
    }

    private void OnMouseDown() {
        if(!IsActive) return;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        //changing the highlightedd size for the potential ship placement 
        potentialShipPlacement.SetPawnSize(currentPawn.GetPawnSize());
    }
    private void OnMouseUp() {   
        if(!IsActive) return;
        dragging = false;
        SnapToCube();
    }
    private void SnapToCube() {
        if (potentialShipPlacement.GetCurrentHighlightedCubeVisual() != null) {
            currentPawn.transform.position =  potentialShipPlacement.GetCurrentHighlightedCubeVisual().GetCubeMidPosition();
            currentPawn.SetPlacedStatus(true);
            OnPawnPlaced?.Invoke();
            playerPawnsUI.UpdateUI(currentPawn.GetPawnSize());
        }
        else
        {
            Debug.Log("you dropped the pawn but you werent over a cube. so bad.");
            currentPawn.SetPlacedStatus(false);
            ResetToOriginalSpawnPosition();
        }
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
