using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
/// <summary>
/// A script to help make game design easier. Kinda like a cheat mode system in the editor
/// </summary>
public class DevMode 
{

    //MenuItem means it now shows up as a button on the top left of the unity editor
    [MenuItem("Dev Mode/Get Power Up 1")]
    public static void GetPowerUp1()
    {
        Debug.Log("Player now has a free powerup 1");
    }
    
    [MenuItem("Dev Mode/Get Power Up 2")]
    public static void GetPowerUp2()
    {
        Debug.Log("Player now has a free powerup 2");
    }
    
    [MenuItem("Dev Mode/Show Enemy Pawn Positions")]
    public static void ShowEnemyPositions()
    {
        Debug.Log("Showing Enemy Positions");
    }
    
    [MenuItem("Dev Mode/Speedy Gameplay")]
    public static void RaeusSpeedMode()
    {
        Debug.Log("Moving at the speed of raeus");
    }
}
