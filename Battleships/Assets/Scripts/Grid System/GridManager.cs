using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   
   private const int WIDTH = 10, HEIGHT = 10;
   [SerializeField][Range(1f, 2f)] private float widthOffset = 1.5f;
   [SerializeField][Range(1f, 2f)] private float heightOffset = 1.5f;
   
   
   [SerializeField] private GameObject tilePrefab;

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
            GameObject spawnedTile = Instantiate(tilePrefab, new Vector3(x * widthOffset, -1 *y * heightOffset), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y}";
            spawnedTile.transform.parent = this.transform;
            //give the tiles a checkerboard effect
            bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
            spawnedTile.GetComponent<TileColor>().Init(isOffset);

            tiles[new Vector2(x, y)] = spawnedTile;
         }
      }
      SetCameraToMiddleOfGrid();
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
   
   /// <summary>
   /// this is quick and dirty code, will be refactored later 
   /// </summary>
   private void SetCameraToMiddleOfGrid()
   {
      Camera.main.transform.position = new Vector3((float)WIDTH/2+ widthOffset, ((float)HEIGHT/2 +heightOffset) * -1, -10);
   }

   public Dictionary<Vector2, GameObject> GetTiles()
   {
      return tiles;
   }
   
}
