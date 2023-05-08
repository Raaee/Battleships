using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{

    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color placementColor;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private GameObject highlight;


    private PotentialShipPlacement potentialShipPlacement;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
    }

    public void Init(bool isOffset)
    {
        sr.color = isOffset ? offsetColor : baseColor;
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
        highlight.SetActive(true);
    }

    public void HideHighlight()
    {
        highlight.SetActive(false);
    }
    
}
