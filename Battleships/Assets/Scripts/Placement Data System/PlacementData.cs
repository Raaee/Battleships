using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
/// <summary>
/// This shows the status of the player/enemy placement positions of pawns 
/// </summary>
public class PlacementData : MonoBehaviour
{
   //an array of pawns, (garanteed 5)
   public List<Pawn> pawns;
   
   //enum of player or enemy 
   [SerializeField] private Team team;


}

public enum Team {
   NONE,
   PLAYER,
   ENEMY
}

