using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnCoordinates : MonoBehaviour
{
    //horizontal 
    private int number;
    //vertical
    private char letter;

    public PawnCoordinates(int number, char letter)
    {
        this.letter = letter;
        this.number = number;
    }



    public string GetCoordinates()
    {
        return  letter + "" + number;
    }
}
