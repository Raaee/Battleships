using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour {

    [SerializeField] TMP_Text currentRoundTxt;
    [SerializeField] TMP_Text whosTurnTxt;
    [SerializeField] TMP_Text shipsRemainingTxt;

    private int currentTurn = 0;
    private GameState currentState;
    
    public void SetCurrentState(GameState currState) {
        currentState = currState;
    }

    public void UpdateTurnTxt() {
       // Debug.Log(currentState);
        if (currentState.stateTeam == StateTeam.PLAYER) {
            whosTurnTxt.text = "Player's Turn";
        } else {
            whosTurnTxt.text = "Enemy's Turn";
        }
    }
    public void UpdateRoundNum(int roundNum, int maxRounds) {
        currentRoundTxt.text = "Round " + roundNum + " / " + (maxRounds+1);
    }

    public void UpdateShipsRemainTxt(int amtShips) {
        shipsRemainingTxt.text = amtShips + " enemy ships remaining.";
    }

}
