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

    private bool isPlaced = false;

    public UnityEvent OnPawnPlaced;

    private void Awake()
    {
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        currentPawn = GetComponent<Pawn>();
    }


    private void Update() {
        if (isPlaced) return;
        
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
        if (isPlaced) return;
        
        dragging = false;
        SnapToCube();
        isPlaced = true;
        OnPawnPlaced?.Invoke();
    }
    private void SnapToCube() {
        if (isPlaced) return;
        
        if (potentialShipPlacement.GetCurrentHighlightedCubeVisual() != null) {
            currentPawn.transform.position =  potentialShipPlacement.GetCurrentHighlightedCubeVisual().GetCubeMidPosition();
        }
        else
        {
            Debug.Log("you dropped the pawn but you werent over a cube. so bad.");
        }
    }

    public bool GetIsDragging()
    {
        return dragging;
    }

   
}
