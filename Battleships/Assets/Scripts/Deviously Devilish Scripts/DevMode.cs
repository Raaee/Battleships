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

   
  
    [MenuItem("Dev Mode/Toggle Enemy Pawn Positions")]
    public static void ToggleEnemyPositions()
    {
        Debug.Log("Showing Enemy Positions");
        var enemies = GameObject.FindObjectOfType<BetterEnemyPlacement>().pawnsInBattle;
        if (enemies == null)
        {
            Debug.Log("cant find anythign bruv");
            return;
        }

        if (enemies.Count <= 0)
        {
            Debug.Log("theres no enemies bruv");
            return;
        }
        
        foreach (var i in enemies)
        {
            if(i.activeInHierarchy == false)
                i.SetActive(true);
            else
            {
                i.SetActive(false);
            }
        }
        
    }

    [MenuItem("Dev Mode/Toggle Enemy Occupied Highlights")]
    public static void ToggleEnemyOccupied() {
        Debug.Log("Showing Enemy's Occupied Highlights");

        Debug.Log("*** Must implment***");

    }
    
    [MenuItem("Dev Mode/Chat G-PTY intrusive thoughts on Raeus")]
    public static void IntrusiveThoughts()
    {
        int rand = Random.Range(0, 1000);
        string noun = getRandomNoun();
        string noun2 = getRandomNoun();
        string adjective = getRandomAdjective();
        string verb = getRandomVerb();
        
        Debug.Log("Chat G-PTY: Raeus is a " + adjective + " " + noun + " who " + verb + " " + noun2 + "s.");
    }

    private static string getRandomNoun()
    {
        string[] randomNouns = new[] { "bug", "worm", "gerbil", "human", "alien", "pasta" }; 
        return randomNouns[Random.Range(0, randomNouns.Length)];
    }
    private static string getRandomAdjective()
    {
        string[] randomNouns = new[] { "vigorous", "beautiful", "disgusting", "parasitic", "venomous", "mid", "disturbed"}; 
        return randomNouns[Random.Range(0, randomNouns.Length)];
    }
    private static string getRandomVerb()
    {
        string[] randomNouns = new[] { "defends", "eats", "flirts with", "talks to", "games with", "decorates", "stalks"}; 
        return randomNouns[Random.Range(0, randomNouns.Length)];
    }
    
    
    [MenuItem("Dev Mode/Speedy Gameplay")]
    public static void RaeusSpeedMode()
    {
        Debug.Log("Moving at the speed of raeus.");
    }
}
