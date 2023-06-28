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
    [Header("DEBUG")]
    [SerializeField] private CubeVisual currentCubeVisual = null;
    [SerializeField] private bool isDraggingPawn = false;
    public List<ClickAndDrag> clickAndDrags;

    private void Update()
    {
        GetIsDraggingPawn();
        
        //if you are dragging a pawn, and you are over cube visual, and its your grid, we can use the highlight system
        //we will need the pawn size you are dragging, and the orientation 
    }

    public bool GetIsDraggingPawn()
    {
        foreach (ClickAndDrag cd in clickAndDrags)
        {
            if (cd.GetIsDragging() == true)
            {
                isDraggingPawn = true;
                return true;
 
            }
        }

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
}
