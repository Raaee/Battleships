using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// during the set up state. tells the player how many of each pawn they have.
/// </summary>
public class AmountOfPlayerPawnsUI : MonoBehaviour
{
    public List<TextMeshProUGUI> uiGameObjects;
    [SerializeField] private PlayerPlacementData playerPlacementData;
    
    private Dictionary<int, int> sizeAmtDictionary;
    private ButtonFunctions buttonFunctions;

    private void Awake()
    {
        buttonFunctions = FindObjectOfType<ButtonFunctions>();
        if (buttonFunctions == null)
        {
            Debug.Log("no button functions script in scene dummy");
            return;
        }
        buttonFunctions.OnPlayerConfirmPlacement.AddListener(RemoveAllButtons);
        playerPlacementData.OnAllPawnsSpawned.AddListener(StartingAmountInUI);
    }

    //key is pawnsize, value is the amount
    private void StartingAmountInUI()
    {
        sizeAmtDictionary = new Dictionary<int, int>();
        foreach (var pawns in playerPlacementData.pawnsInBattle)
        {
            int pawnSize = pawns.GetComponent<Pawn>().GetPawnSize();
            if (!sizeAmtDictionary.ContainsKey(pawnSize)) //if doesnt exist yet, add it to the dictionary
            {
                sizeAmtDictionary.Add(pawnSize, 1);
            }
            else
            {
                sizeAmtDictionary[pawnSize] = sizeAmtDictionary[pawnSize] + 1;
            }
        }

        for (int i = 0; i < uiGameObjects.Count; i++)
        {
            int amt = 0;

            if (sizeAmtDictionary.ContainsKey(i + 1))
            {
                amt = sizeAmtDictionary[i + 1];
               
            }
           
            uiGameObjects[i].text = "x " + amt;
        }
        
        
    }

    public void UpdateUI(int pawnSize)
    {
        if (sizeAmtDictionary.ContainsKey(pawnSize))
        {
            sizeAmtDictionary[pawnSize] -= 1;
            if (sizeAmtDictionary[pawnSize] <= 0)
                sizeAmtDictionary[pawnSize] = 0;
        }

        uiGameObjects[pawnSize - 1].text = "x " + sizeAmtDictionary[pawnSize];

    }

    private void RemoveAllButtons()
    {
        foreach (var uiElement in uiGameObjects)
        {
            uiElement.gameObject.SetActive(false);
        }
    }
}
