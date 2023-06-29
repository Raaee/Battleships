using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// what is the mouse hovering over? this script will hopefully solve that.
/// </summary>
public class InputData : MonoBehaviour
{
    [SerializeField] private GridManager playerGridmanager; 
    [Header("DEBUG")]
    [SerializeField] private CubeVisual currentCubeVisual = null;

    [SerializeField] private PawnVisual currentPawnVisual = null;
    [SerializeField] private bool isDraggingPawn = false;
    
    public List<ClickAndDrag> clickAndDrags;

    private void Update()
    {
        GetIsDraggingPawn();
        
        //if you are dragging a pawn,
        if (isDraggingPawn == false) return;
        //and you are over cube visual,
        if(currentCubeVisual == null) return;
        //and its your grid,
        if(DoesCubeBelongToPlayerGrid() == false) return;
        
        
        //we can use the highlight system
        ShowHighlight();
      
    }

    private void ShowHighlight()
    {
        //we will need the pawn size you are dragging, and the orientation 
        PawnOrientation pawnOrientation = currentPawnVisual.GetPawnOrientation();
        int pawnSize = currentPawnVisual.gameObject.GetComponent<Pawn>().GetPawnSize();
        
        //and then we just make the highlight system work its magic
        //TODO: refactor part of the highlight so that we can call it from here
        
    }

    public bool GetIsDraggingPawn()
    {
        
        foreach (ClickAndDrag cd in clickAndDrags)
        {
            if (cd.GetIsDragging() == true)
            {
                currentPawnVisual = cd.gameObject.GetComponent<PawnVisual>();
                isDraggingPawn = true;
                return true;
 
            }
        }

        currentPawnVisual = null;
        isDraggingPawn = false;
        return false;
    }
    public void SetCubeVisual(CubeVisual cubeVisual)
    {
        currentCubeVisual = cubeVisual;
    }

    public void ResetCubeVisual()
    {
        currentCubeVisual = null;
    }

    public void AddSelfToClickDragList(ClickAndDrag cd)
    {
        clickAndDrags.Add(cd);
    }

    private bool DoesCubeBelongToPlayerGrid()
    {
        if (currentCubeVisual == null) return false;

        if (playerGridmanager.GetPositionAtTile(currentCubeVisual.gameObject).x <= -1)
        {
            return false;
        }

        return true;
    }
}
