using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Pawn : MonoBehaviour
{
    //size 
    [SerializeField] [Range(1, 5)] private int pawnSize = 1;
    [SerializeField] Transform pawnFloorPos;

    [SerializeField] private Vector3 pawnVisualOffset;
    //position - list of PawnCoordinates 
    public List<Vector2Int> pawnCoords;
    public Vector3 GetPawnFloorPos() {
        return pawnFloorPos.transform.position;
    }

    public Vector3 GetPawnVisualOffset()
    {
        return pawnVisualOffset;
    }
}
