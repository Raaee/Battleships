using System;
using UnityEngine;
/// <summary>
/// 2. when objects are placed it sends its data to placement data 
/// </summary>
public class ClickAndDrag : MonoBehaviour
{
   
    private bool dragging = false;
    private Vector3 offset;

     private PotentialShipPlacement potentialShipPlacement;
    private Pawn currentPawn;
  

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
    }
    private void SnapToCube() {
        if (potentialShipPlacement.GetCurrentHighlightedCubeVisual() != null) {
            currentPawn.transform.position =  potentialShipPlacement.GetCurrentHighlightedCubeVisual().GetCubeMidPosition();
        }
    }

    public bool GetIsDragging()
    {
        return dragging;
    }

   
}
