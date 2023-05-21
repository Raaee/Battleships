using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Pawn : MonoBehaviour
{
    //size 
    [SerializeField] [Range(1, 5)] private int pawnSize = 1;
    

    //position - list of PawnCoordinates 
    public List<Vector2Int> pawnCoords;

    private ClickAndDrag cd;
    private PotentialShipPlacement potentialShipPlacement;
    private void Awake()
    {
        cd = GetComponent<ClickAndDrag>();
        cd.OnPawnPlaced.AddListener(SetPawnCoordinates);
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
    }

    public void SetPawnCoordinates()
    {
        List<GameObject> lastHighlightedGameobjects = potentialShipPlacement.GetLastHighlightedObjects();
        //TODO: convert the gameobjects into the specific pawncords 
        
    }
    
    public int GetPawnSize() {
        return pawnSize;
    }
}
