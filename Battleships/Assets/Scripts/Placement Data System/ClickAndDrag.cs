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

    private bool IsActive = true;
    private ButtonFunctions buttonsFunctions;


    private void Awake()
    {
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        currentPawn = GetComponent<Pawn>();
        buttonsFunctions = FindObjectOfType<ButtonFunctions>(); 
        buttonsFunctions.OnPlayerConfirmPlacement.AddListener(DisableSelf);
    }

    private void DisableSelf()
    {
        IsActive = false;
        
    }


    private void Update() {
        if(!IsActive) return;
        
        
        if (dragging) {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPos;
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
