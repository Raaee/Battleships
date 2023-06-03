using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Grid Palette", fileName = "NewGridPalette")]
public class GridSystemCubeSO : ScriptableObject
{
   //the mid ass normal cube
   public GameObject defaultCube;
   
   //the 4 corner edge cubes
   public GameObject CornerBotLeftCube;
   public GameObject CornerBotRightCube;
   public GameObject CornerTopLeftCube;
   public GameObject CornerTopRightCube;
   
   //the four edge side cubes 
   public GameObject EdgeTopCube;
   public GameObject EdgeBotCube;
   public GameObject EdgeLeftCube;
   public GameObject EdgeRightCube;
   
}
