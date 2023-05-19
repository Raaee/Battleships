using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A grid manager script that will be set in 3d to achieve a 2.5d effect
/// </summary>
public class GridManager : MonoBehaviour
{
   
   private const int WIDTH = 10, HEIGHT = 10;
   private float widthOffset = 1.15f;
   private float heightOffset = 1.3f;
   
   
   [SerializeField] private GameObject tilePrefab;
   [SerializeField] private GameObject startingTilePoint;

   private Dictionary<Vector2, GameObject> tiles;

   private void Start()
   {
      GenerateGrid();
   }

   private void GenerateGrid()
   {
      tiles = new Dictionary<Vector2, GameObject>();
      for (int x = 0; x < WIDTH; x++)
      {
         for (int y = 0; y < HEIGHT; y++)
         {
            Vector3 startingTileLoc = startingTilePoint.transform.position; // this is here so we spawn the grid based on this gameobjects position
            GameObject spawnedTile = Instantiate(tilePrefab, new Vector3(x * widthOffset  + startingTileLoc.x, 0  + startingTileLoc.y,y * heightOffset + startingTileLoc.z), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y}";
            spawnedTile.transform.parent = this.transform;
           
            tiles[new Vector2(x, y)] = spawnedTile;
         }
      }
      
   }

   public GameObject GetTileAtPosition(Vector2 pos)
   {
      if (tiles.TryGetValue(pos, out var tile))
      {
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
   
   
   
  
}
