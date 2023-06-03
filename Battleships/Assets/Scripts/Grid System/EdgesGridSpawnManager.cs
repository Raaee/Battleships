using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgesGridSpawnManager : MonoBehaviour
{
   public GameObject bottomEdgeCube;
   public GameObject regularCube;

   public GameObject GetCorrectCube(int x, int y)
   {
      if (y == 0)
         return bottomEdgeCube;
   
      return regularCube;
   }
}
