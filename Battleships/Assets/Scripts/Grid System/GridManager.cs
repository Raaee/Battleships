using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The GridManager class is responsible for generating and managing a 2.5D grid system in a Unity project. 
/// It creates a grid of tiles with specified dimensions and offsets, using different types of cubes based on their positions (corners, edges, and default). 
/// The class provides methods for getting a tile at a specific position, getting the position of a tile, and accessing the entire grid.
/// </summary>
public class GridManager : MonoBehaviour
{
   
   private const int WIDTH = 10, HEIGHT = 10;
   private const float widthOffset = 1.125f;//1.15
   private const float heightOffset = 1.275f;//1.3

   [SerializeField] private GameObject startingTilePoint;

   public Dictionary<Vector2, GameObject> tiles;

   [SerializeField] private GridSystemCubeSO cubePalette;

   public void GenerateGrid()
   {
      tiles = new Dictionary<Vector2, GameObject>();
        // this is here so we spawn the grid based on this gameobjects position
        Vector3 startingTileLoc = startingTilePoint.transform.position;
        for (int x = 0; x < WIDTH; x++)
      {
         for (int y = 0; y < HEIGHT; y++)
         {
            //if x is this and y is that, do a differnt logic to make spawn edges 
            
            
            GameObject spawnedTile = Instantiate(GetCorrectCube(x, y), new Vector3(x * widthOffset  + startingTileLoc.x, 0  + startingTileLoc.y,y * heightOffset + startingTileLoc.z), Quaternion.identity); 
            spawnedTile.name = $"Tile {x} {y} - " + spawnedTile.name;
            spawnedTile.transform.parent = this.transform;
            
            tiles[new Vector2(x, y)] = spawnedTile;
         }
      }
   }

   

   public GameObject GetTileAtPosition(Vector2 pos)
   {
        if (tiles == null) {
            GenerateGrid();
        }
        if (tiles.TryGetValue(pos, out var tile)) {
            return tile;
        }
        return null;
   }


   public Vector2 GetPositionAtTile(GameObject go)
   {

      if (!tiles.ContainsValue(go))//gameobject dont exist in dictionary 
      {
         return new Vector2(-1, -1); 
      }

      foreach (var vec2 in tiles) //iterate through the dictionary 
      {
         if (GameObject.ReferenceEquals(vec2.Value, go)) //if the gameobject is equal to this keys value
         {
            return vec2.Key; //return key 
         }
      }

      return new Vector2(-1, -1); //this is like return null
   }
   
 
   public Dictionary<Vector2, GameObject> GetTiles()
   {
      return tiles;
   }
   private GameObject GetCorrectCube(int x, int y)
   {
      //first cover all the corner edges 
      if (x == 0 && y == 0)
         return cubePalette.CornerBotLeftCube;
      
      if (x == 0 && y == 9)
         return cubePalette.CornerTopLeftCube;
      
      if (x == 9 && y == 9)
         return cubePalette.CornerTopRightCube;
      
      if (x == 9 && y == 0)
         return cubePalette.CornerBotRightCube;
      
      //next cover the edges
      if (x <= 9 && (y == 0))
         return cubePalette.EdgeBotCube;
      
      if(x <=9  && (y == 9))
         return cubePalette.EdgeTopCube;

      if (x == 0 && (y <= 9))
         return cubePalette.EdgeLeftCube;

      if (x == 9 && (y <= 9))
         return cubePalette.EdgeRightCube;
      
      
      //finally, all other cubes will be the default cube
      return cubePalette.defaultCube;
   }
  
}
