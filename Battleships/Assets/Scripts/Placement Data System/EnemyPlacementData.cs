using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlacementData : MonoBehaviour
{
    //an array of pawns
    public List<GameObject> pawnPrefabs; // this is the default pawn prefabs used to assign the player's army
    public List<GameObject> pawnsInBattle; // this will be the enemy's pawns

    [SerializeField] private GridManager enemyGridManager; 
    
    
    
    
    //TODO: spawn enemy prefabs, disactivate them so player cant see them
    
    
    //TODO: logic to make enemy prefabs be assigned to a place on a board
}
