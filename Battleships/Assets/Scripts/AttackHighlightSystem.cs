using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHighlightSystem : MonoBehaviour
{
    [SerializeField] private bool isEnabled = false;
    private CubeVisual currentHighlightedVisual = null;
    public Material hoverAttackMat;
    [SerializeField] private GridManager gridManager;
    public void EnableSystem()
    {
        isEnabled = true;
    }

    public void DisableSystem()
    {
        isEnabled = false;
    }


    public void AssignCurrentVisual(CubeVisual cv)
    {
        if(isEnabled == false) return;
        if (!IsCubeInGridManager(cv)) return;
        
        cv.ShowHighlight(hoverAttackMat);
    }
    
    public void RemoveCurrentVisual(CubeVisual cv)
    {
        if(isEnabled == false) return;
        
        cv.HideHighlight();
    }

    public bool IsCubeInGridManager(CubeVisual cv)
    {
        if (gridManager.GetPositionAtTile(cv.gameObject).x < 0)
            return false;
        
        return true; 
    }
    
   
    
}
