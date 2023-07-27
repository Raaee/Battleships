using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//just chooses a random location, no context to its attack
public class StupidEnemyAI : IEnemyAI
{ 
    [SerializeField] private GridManager playerGridManager;

    public override Vector2 DetermineNextLocation()
    {
        var loc = GetRandomVector2(0,  10, 0 , 10);
        //while "cube state  is miss" get another random place 
        var tile = playerGridManager.GetTileAtPosition(loc);
        while( tile.GetComponent<CubeVisual>().GetCubeHitState() == CubeHitState.MISS)
        {
            Debug.Log("tried to attak same place so we are going through the while loop");
            loc = GetRandomVector2(0, 10, 0, 10);
            tile = playerGridManager.GetTileAtPosition(loc);
        }

        return loc;
    }

   

    public Vector2 GetRandomVector2(int minX, int maxExclusiveX, int minY, int maxExclusiveY)
    {
        return new Vector2(Random.Range(minX, maxExclusiveX), Random.Range(minY, maxExclusiveY));
    }

}
