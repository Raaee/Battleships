using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The visual functions of the cube.
/// </summary>
public class CubeVisual : MonoBehaviour
{

    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material missMaterial;
    [SerializeField] private Material occupiedMaterial;

   // private Material currentMat;

    private CubeHitState cubeHitState = CubeHitState.NONE;
    [SerializeField] private Material currentMat;

    [SerializeField] private Transform cubeMidpoint;
    
    private bool isOccupied;

    private PotentialShipPlacement potentialShipPlacement;
    private AttackHighlightSystem attackHighlightSystem;

    private void Awake()
    {
        attackHighlightSystem = FindObjectOfType<AttackHighlightSystem>();
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        GetComponent<Renderer>().material = originalMaterial;
        currentMat = originalMaterial;
        
    }   

    private void OnMouseEnter()
    {
      // ShowHighlight();      
       potentialShipPlacement.AssignCurrentTileVisual(this);
       attackHighlightSystem.AssignCurrentVisual(this);
    }
    private void OnMouseExit()
    {
      // HideHighlight();
       potentialShipPlacement.RemoveCurrentTileVisual();
       attackHighlightSystem.RemoveCurrentVisual(this);
    }

    public void ShowHighlight() {
        GetComponent<Renderer>().material = hoverMaterial;
    }

    public void ShowHighlight(Material newMaterial) {
        GetComponent<Renderer>().material = newMaterial;
    }
    
    public void HideHighlight() {
       // Debug.Log("back to normal dummy ");
        GetComponent<Renderer>().material = currentMat;
    }

    public Vector3 GetCubeMidPosition()
    {
        return cubeMidpoint.transform.position;
    }
    public bool GetIsOccupied() {
        return isOccupied;
    }
    public void SetIsOccupied(bool o) {
        isOccupied = o;
    }

    // This is for the attack selection stuff:
    private void OnMouseDown()  {
        attackHighlightSystem.SetCurrentlyHighlighted(this);
    }
    private void OnMouseUp() {
        if (attackHighlightSystem.isEnabled)
            FindObjectOfType<PlayerActionState>().SetAttackLocation();
    }
    public void ChangeMaterialOnHitState(CubeHitState hitState) {
        switch (hitState) {
            case CubeHitState.NONE: currentMat = originalMaterial;
                break;
            case CubeHitState.HIT: currentMat = hitMaterial;
                break;
            case CubeHitState.MISS: currentMat = missMaterial;
                break;
            case CubeHitState.OCCUPIED: currentMat = occupiedMaterial;
                break;
        }
        GetComponent<Renderer>().material = currentMat;
    }
}

public enum CubeHitState {
    NONE,
    HIT,
    MISS,
    OCCUPIED
}



