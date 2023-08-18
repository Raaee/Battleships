using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Almost like a queeue system. basic idea is if queue is empty, just choose random points, else choose the next possible location.
/// if it the point you hit was a valid  location, then add its 4 corners to the next possible locationss
/// </summary>
public class NormalEnemyAI : IEnemyAI
{
    private List<Vector2> nextPossibleLocations;

    private List<Vector2> hitHistory;
      // Start is called before the first frame update
    void Start()
    {
        nextPossibleLocations = new List<Vector2>();
        hitHistory = new List<Vector2>();
    }

   

    public override Vector2 DetermineNextLocation()
    {
       //
       if (nextPossibleLocations.Count <= 0)
       {
           //Vector2 randVec2 = RandomVector2();
          // hitHistory.Add(randVec2);
           //return randVec2; 

       }
       else
       {
           Vector2 possibleLocation = nextPossibleLocations[0];
           nextPossibleLocations.RemoveAt(0);
           hitHistory.Add(possibleLocation);
           return possibleLocation;
          
       }
       return Vector2.zero;
    }


    private void Add4Corners(Vector2 hitVector2) //event listener based on if there was a succesful hit 
    {
        //make a size 4 array with all the hitVector2 directions 
        
        //iterate through this array, if its a valid place in the gridmanager and it wasnt already hit , then add it to next possible locations
    }
}
