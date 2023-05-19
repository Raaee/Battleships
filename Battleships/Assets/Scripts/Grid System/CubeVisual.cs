using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVisual : MonoBehaviour
{

    private Material originalMaterial;
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Transform cubeMidpoint;

    private PotentialShipPlacement potentialShipPlacement;
    
    private void Awake()
    {
       
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        originalMaterial = GetComponent<Renderer>().material;
    }   

    private void OnMouseEnter()
    {
       ShowHighlight();
       potentialShipPlacement.AssignCurrentTileVisual(this);
    }
    
    private void OnMouseExit()
    {
       HideHighlight();
       potentialShipPlacement.RemoveCurrentTileVisual();
    }

    public void ShowHighlight()
    {

        GetComponent<Renderer>().material = hoverMaterial;
    }

    public void HideHighlight()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }

    public Vector3 GetCubeMidPosition()
    {
        return cubeMidpoint.transform.position;
    }
    
}
