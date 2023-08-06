using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The simplest implementation of the enemy AI. It already knows where the players pawns are and will have a percentage to try to hit it 
/// </summary>
public class BasicEnemyAI : IEnemyAI
{ 
    [SerializeField] private GridManager playerGridManager;

    void Start() {
        var enemyAiStats = FindObjectOfType<EnemyAiStats>();
        if(enemyAiStats)
            percentageToHit = FindObjectOfType<EnemyAiStats>().GetPercentageToHit();
        else
        {
            percentageToHit = 0.15f;
        }
           
    }

    public override Vector2 DetermineNextLocation()
    {
        //get the pawn we want to hit if it was a garanteed hit

        List<Vector2> allPotentialCoordsToHit = new List<Vector2>();
        foreach (GameObject go in playerPlacementData.pawnsInBattle)
        {
            Pawn pawn = go.GetComponent<Pawn>();
            if (pawn == null) Debug.Log("pawn is null");

            foreach (Vector2 pawnCoord in pawn.pawnCoords)
            {
                allPotentialCoordsToHit.Add(pawnCoord);
            }
        }

        int amtOfPawnPieces = allPotentialCoordsToHit.Count;
        if (amtOfPawnPieces <= 0)
        {
            Debug.Log("nothing left to hit bruv");
            return GetRandomGridLocation();
        }

        //The gareented vector 2 we will hit if we want to
        Vector2 garanteedHitVec2 = allPotentialCoordsToHit[Random.Range(0, amtOfPawnPieces)];

        //ah yes a 100 sided dice
        float diceRoll = Random.Range(0.01f, 1f);
        if(diceRoll < percentageToHit)
        {
            //we hit that foo 
            //we continue 
        }
        else
        {
            //we missed that foo, but lets see if we passed our garanteed hits
            currentMisses += 1;

            if(currentMisses > allowedMisses)
            {
                currentMisses = 0;
                //and then we still continue
            }
            else
            {
                return GetRandomGridLocation();
            }          

            
        }

        //if we reached here we can finally hit our target! 
        return garanteedHitVec2;

    }

    private Vector2 GetRandomGridLocation()
    {
        //hit random locatoin
        var randomLoc = GetRandomVector2(0, 10, 0, 10);
        //while "cube state  is miss" get another random place 
        var tile = playerGridManager.GetTileAtPosition(randomLoc);
        while (tile.GetComponent<CubeVisual>().GetCubeHitState() == CubeHitState.MISS)
        {
            Debug.Log("tried to attak same place so we are going through the while loop");
            randomLoc = GetRandomVector2(0, 10, 0, 10);
            tile = playerGridManager.GetTileAtPosition(randomLoc);
        }

        return randomLoc;
    }

    public Vector2 GetRandomVector2(int minX, int maxExclusiveX, int minY, int maxExclusiveY)
    {
        return new Vector2(Random.Range(minX, maxExclusiveX), Random.Range(minY, maxExclusiveY));
    }

}
