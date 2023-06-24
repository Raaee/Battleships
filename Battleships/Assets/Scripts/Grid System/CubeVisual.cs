using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The visual functions of the cube.
/// </summary>
public class CubeVisual : MonoBehaviour
{

    private Material originalMaterial;
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material missMaterial;

    [SerializeField] private Transform cubeMidpoint;
    
    private bool isOccupied;

    private PotentialShipPlacement potentialShipPlacement;
    private AttackHighlightSystem attackHighlightSystem;

    private void Awake()
    {
        attackHighlightSystem = FindObjectOfType<AttackHighlightSystem>();
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        originalMaterial = GetComponent<Renderer>().material;
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

    public void ShowHighlight()
    {

        GetComponent<Renderer>().material = hoverMaterial;
    }

    public void ShowHighlight(Material newMaterial)
    {
        GetComponent<Renderer>().material = newMaterial;
    }
    
    public void HideHighlight()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
    public void ShowCubeHitVisul() {
        GetComponent<Renderer>().material = hitMaterial;
    }
    public void ShowCubeMissVisual() {
        GetComponent<Renderer>().material = missMaterial;
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

    private void OnMouseDown()  {
        attackHighlightSystem.SetCurrentlyHighlighted(this);
    }
    private void OnMouseUp() {
        if (attackHighlightSystem.isEnabled)
            FindObjectOfType<PlayerActionState>().SetAttackLocation();
    }
}
