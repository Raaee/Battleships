using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotentialShipPlacement : MonoBehaviour
{
   [SerializeField] private TileVisual currentHighlightedTileVisual = null;
   [SerializeField] private GridManager gridManager;

   private bool isVertical = true;
  [SerializeField][Range(1, 6)] private int sizeOfPawn = 2;

  [SerializeField] private TextMeshProUGUI alignmentText;
  [SerializeField] private TextMeshProUGUI sizeText;


  private void Start()
  {
     UpdateUI();
  }

  private void ShowPotentialShipPlacement()
   {

       Vector2 currTilePos = gridManager.GetPositionAtTile(currentHighlightedTileVisual.gameObject);
       if (isVertical) 
       {
           if (currTilePos.y + sizeOfPawn > 10) 
           {
               RemoveAllHighlights();
               return;
           }
           //now we can highlight 
           HighlightPotentialShipPlacementVert((int)currTilePos.x, (int)currTilePos.y);
       }
       else //we want to do samething but horizontally 
       {
           if (currTilePos.x + sizeOfPawn > 10)
           {
               RemoveAllHighlights();
               return;
           }
           //now we can highlight horizontally 
           HighlightPotentialShipPlacementHori((int)currTilePos.x, (int)currTilePos.y);

       }
   }

   private void HighlightPotentialShipPlacementVert(int x,int y)
   {
       //start at current tile, and highlight the next tiles to the right for size of pawn times 
       for (int i = y; i < (y + sizeOfPawn); i++)
       {
           var tileGO = gridManager.GetTileAtPosition(new Vector2(x, i));
           if(tileGO)
            tileGO.GetComponent<TileVisual>().ShowHighlight();
       }
   }

   private void HighlightPotentialShipPlacementHori(int x, int y)
   {
       for (int i = x; i < (x + sizeOfPawn); i++)
       {
           var tileGO = gridManager.GetTileAtPosition(new Vector2(i, y));
           
           if(tileGO)
               tileGO.GetComponent<TileVisual>().ShowHighlight();
       }
   }
   

    public void AssignCurrentTileVisual(TileVisual tileVisual)
    {
        currentHighlightedTileVisual = tileVisual;
        ShowPotentialShipPlacement();
    }
    
    public void RemoveCurrentTileVisual()
    {
        currentHighlightedTileVisual = null;
        RemoveAllHighlights();
    }

    private void RemoveAllHighlights()
    {
        foreach (var go in gridManager.GetTiles())
        {
            var tileColor = go.Value.gameObject.GetComponent<TileVisual>();
            if (tileColor != null)
            {
                tileColor.HideHighlight();
            }
        }
    }

    public void ToggleShipOrientation()
    {
        isVertical = !isVertical;
        UpdateUI();
    }

    public void ChangeShipAmount()
    {
        sizeOfPawn++;
        if (sizeOfPawn > 5)
        {
            sizeOfPawn = 1;
        }

       UpdateUI();
    }

    private void UpdateUI()
    {
        sizeText.text = sizeOfPawn.ToString();
        alignmentText.text = isVertical ? "Vertical" : "Horizontal"; 
    }
}
