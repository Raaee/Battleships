using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotentialShipPlacement : MonoBehaviour
{
   [SerializeField] private TileColor currentHighlightedTileColor = null;
   [SerializeField] private GridManager gridManager;

   private bool isVertical = true;
  [SerializeField][Range(1, 6)] private int sizeOfPawn = 2;

  [SerializeField] private TextMeshProUGUI alignmnetText;
  [SerializeField] private TextMeshProUGUI sizeText;


  private void Start()
  {
     UpdateUI();
  }

  private void ShowPotentialShipPlacement()
   {
       Vector2 tileVec2 = gridManager.GetPositionAtTile(currentHighlightedTileColor.gameObject);
       if (isVertical) //highlight to the right of the mouse tile for the size of Pawn 
       {
            //we know the grid is 10x10
            //if the current hightlighted x position is ... we return 
            //get y value if current tile 
            float currentTileY = tileVec2.y;

            if (currentTileY + sizeOfPawn > 10)
            {
              //cant highlight 
                Debug.Log("Cannot highlight mf, should we remove all highlights?");
                RemoveAllHighlights();
                return;
            }
            //now we can highlight 
            HighlightPotentialShipPlacementVert((int)tileVec2.x, (int)tileVec2.y);
       }
       else //we want to do samething but horizontally 
       {
           float currentTileX = tileVec2.x;
           if (currentTileX + sizeOfPawn > 10)
           {
               //cant highlight 
               Debug.Log("Cannot highlight mf, should we remove all highlights?");
               RemoveAllHighlights();
               return;
           }
           //now we can highlight horizontally 
           HighlightPotentialShipPlacementHori((int)tileVec2.x, (int)tileVec2.y);

       }
   }

   private void HighlightPotentialShipPlacementVert(int x,int y)
   {
       //start at current tile, and highlight the next tiles to the right for size of pawn times 
       for (int i = y; i < (y + sizeOfPawn); i++)
       {
         
           var tileGO = gridManager.GetTileAtPosition(new Vector2(x, i));
           
           if(tileGO)
            tileGO.GetComponent<TileColor>().ShowHighlight();
       }
   }

   private void HighlightPotentialShipPlacementHori(int x, int y)
   {
       for (int i = x; i < (x + sizeOfPawn); i++)
       {
           var tileGO = gridManager.GetTileAtPosition(new Vector2(i, y));
           
           if(tileGO)
               tileGO.GetComponent<TileColor>().ShowHighlight();
       }
   }
   

    public void AssignCurrentTileColor(TileColor tileColor)
    {
        currentHighlightedTileColor = tileColor;
        ShowPotentialShipPlacement();
    }
    
    public void RemoveCurrentTileColor()
    {
        currentHighlightedTileColor = null;
        RemoveAllHighlights();
    }

    private void RemoveAllHighlights()
    {
        foreach (var go in gridManager.GetTiles())
        {
            var tileColor = go.Value.gameObject.GetComponent<TileColor>();
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
        alignmnetText.text = isVertical ? "Vertical" : "Horizontal"; 
    }
}
