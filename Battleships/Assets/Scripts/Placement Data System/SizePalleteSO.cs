using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SizePalleteSO", order = 1)]
public class SizePalleteSO : ScriptableObject
{
    public List<SizePawnEnum> sizeOfPawns;
   
}
