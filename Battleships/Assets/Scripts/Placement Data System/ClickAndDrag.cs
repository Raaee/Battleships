using System;
using UnityEngine;
/// <summary>

///  1. a lot of changes to the visuals of the pawn in this sript, make some sort of PawnVisual class to create a method that toggles the sprite chnages
/// 2. when objects are placed it sends its data to placement data 
/// </summary>
public class ClickAndDrag : MonoBehaviour
{
   
    private bool dragging = false;
    private Vector3 offset;

     private PotentialShipPlacement potentialShipPlacement;
    private Pawn currentPawn;
    private SpriteRenderer pawnSP;
  

    private void Awake()
    {
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        currentPawn = GetComponent<Pawn>();
        pawnSP = currentPawn.GetComponent<SpriteRenderer>();
    }


    private void Update() {
        if (dragging) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            UpdatePawnVisual();
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
    }
    private void SnapToCube() {
        if (potentialShipPlacement.GetCurrentHighlightedCubeVisual() != null) {
            currentPawn.transform.position =  potentialShipPlacement.GetCurrentHighlightedCubeVisual().GetCubeMidPosition();
        }
    }

    //this should be in a different class 
    private void UpdatePawnVisual()
    {
        pawnSP.sprite = potentialShipPlacement.GetPawnOrientation() == PawnOrientation.HORIZONTAL ? currentPawn.horizontalSprite : currentPawn.verticalSprite;
    }

}
