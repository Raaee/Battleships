using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IEnemyAI :MonoBehaviour
{
    [SerializeField] [Range(0.1f, 0.66f)] protected float percentageToHit = 0.40f;
    protected float allowedMisses; //this is the max amount of misses before we force AI to hit. This is to make it closer to the percentage! 
    [SerializeField] protected PlayerPlacementData playerPlacementData;
    protected int currentMisses = 0;
   



    public abstract Vector2 DetermineNextLocation();

    private void Awake()
    {
        if (!playerPlacementData)
            playerPlacementData = FindObjectOfType<PlayerPlacementData>();

        
        allowedMisses = (1f / percentageToHit) + 1.5f;
       
    }


   
    
}
