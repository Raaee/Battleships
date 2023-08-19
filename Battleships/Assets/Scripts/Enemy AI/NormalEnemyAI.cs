using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Almost like a queeue system. basic idea is if queue is empty, just choose random points, else choose the next possible location.
/// if it the point you hit was a valid  location, then add its 4 corners to the next possible locationss
/// </summary>
public class NormalEnemyAI : IEnemyAI
{
    
    //issues
    //killing a pawn should remove next possible locations?
    
    //it should not repeat attacking places 
    private List<Vector2> nextPossibleLocations;

    private List<Vector2> hitHistory;
    [SerializeField] private GridManager playerGridManager;

    private bool isPawnDestroyed = false;
    private CPUActionState cpuActionState;
    private PlayerPlacementData playerPlacementData;
    void Start()
    {
        nextPossibleLocations = new List<Vector2>();
        hitHistory = new List<Vector2>();
        cpuActionState = FindObjectOfType<CPUActionState>();
        cpuActionState.OnSuccessfulEnemyHit.AddListener(Add4Corners);
        playerPlacementData = FindObjectOfType<PlayerPlacementData>();
        playerPlacementData.OnPawnFullDestroyed.AddListener(PawnDestroyed);
    }

    private void PawnDestroyed()
    {
        isPawnDestroyed = true;
    }


    public override Vector2 DetermineNextLocation()
    {
        Debug.Log("determining the next location");
        if (nextPossibleLocations.Count <= 0)
        {
            Vector2 randVec2 = PeteHelper.BasicRandomVector2(0,10, 0,10);
            hitHistory.Add(randVec2);
            return randVec2;
        }
        else
        {
            Vector2 possibleLocation = nextPossibleLocations[0];
            nextPossibleLocations.RemoveAt(0);
            hitHistory.Add(possibleLocation);
            return possibleLocation;
          
        }
       
    }


    private void Add4Corners(Vector2 hitVector2) //event listener based on if there was a succesful hit 
    {
        if (isPawnDestroyed)
        {
            isPawnDestroyed = false;
            return;
        }
        
        Debug.Log("Adding the 4 corners");
        //make a size 4 array with all the hitVector2 directions 
        List<Vector2> cornersVectors = new List<Vector2>()
        {
            new Vector2(hitVector2.x, hitVector2.y + 1),//up
            new Vector2(hitVector2.x, hitVector2.y - 1),//down
            new Vector2(hitVector2.x + 1, hitVector2.y),//right
            new Vector2(hitVector2.x - 1, hitVector2.y)//left
        };

        //iterate through this array, if its a valid place in the gridmanager and it wasnt already hit , then add it to next possible locations
        foreach (Vector2 vec2 in cornersVectors)
        {
            if (playerGridManager.GetTileAtPosition(vec2) != null && (!hitHistory.Contains(vec2)))
            {
                nextPossibleLocations.Add(vec2);
            }
        }

        
    }
}
