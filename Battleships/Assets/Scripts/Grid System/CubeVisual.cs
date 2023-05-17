using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVisual : MonoBehaviour
{

    private Material originalMaterial;
    [SerializeField] private Material hoverMaterial;

    private PotentialShipPlacement potentialShipPlacement;
    
    private void Awake()
    {
       
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        originalMaterial = GetComponent<Renderer>().material;
    }

    

    private void OnMouseEnter()
    {
       ShowHighlight();
      // potentialShipPlacement.AssignCurrentTileVisual(this);
    }
    
    private void OnMouseExit()
    {
       HideHighlight();
       //potentialShipPlacement.RemoveCurrentTileVisual();
    }

    public void ShowHighlight()
    {
        //highlight.SetActive(true);
        GetComponent<Renderer>().material = hoverMaterial;
    }

    public void HideHighlight()
    {
        //highlight.SetActive(false);
        GetComponent<Renderer>().material = originalMaterial;
    }
    
}
