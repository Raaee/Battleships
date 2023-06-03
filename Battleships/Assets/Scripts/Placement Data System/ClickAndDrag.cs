using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class ClickAndDrag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    private PotentialShipPlacement potentialShipPlacement;
    private Pawn currentPawn;

    public UnityEvent OnPawnPlaced;

    private void Awake()
    {
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        currentPawn = GetComponent<Pawn>();
    }


    private void Update() {        
        if (dragging) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown() {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        //changing the highlightedd size for the potential ship placement 
        potentialShipPlacement.SetPawnSize(currentPawn.GetPawnSize());
    }
    private void OnMouseUp() {        
        dragging = false;
        SnapToCube();
        OnPawnPlaced?.Invoke();
    }
    private void SnapToCube() {
        if (potentialShipPlacement.GetCurrentHighlightedCubeVisual() != null) {
            currentPawn.transform.position =  potentialShipPlacement.GetCurrentHighlightedCubeVisual().GetCubeMidPosition();
            currentPawn.SetPlacedStatus(true);
        }
        else
        {
            Debug.Log("you dropped the pawn but you werent over a cube. so bad.");
            currentPawn.SetPlacedStatus(false);
        }
    }

    public bool GetIsDragging()
    {
        return dragging;
    }

   
}
