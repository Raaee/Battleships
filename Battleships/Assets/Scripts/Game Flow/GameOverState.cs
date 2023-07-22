using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This class checks who won and shows the respective data for winning 
/// </summary>
public class GameOverState : GameState
{

    [SerializeField] private BetterEnemyPlacement enemyPD;
    [SerializeField] private PlayerPlacementData playerPD;
    [SerializeField] private GameObject gameoverDisplay;
    [SerializeField] private TextMeshProUGUI gameoverText;

    private WinningTeam winningTeam = WinningTeam.NONE;
    public override void OnStateEnter() {
        CheckWhoWon();
        ShowGameOverDisplay();
    }

    public override void OnStateExit()  {
    }

    public override void OnStateUpdate() {

    }
    public override void TurnComplete() {
    
    }
    public override IEnumerator WaitForSec(float time) {
        yield return PeteHelper.GetWait(time);
    }
    public void CheckWhoWon()
    {
        if (enemyPD.GetNumOfPawnsInBattle() <= 0)
        {
            winningTeam = WinningTeam.PLAYERWON;
            return;
        }

        if (playerPD.GetNumOfPawnsInBattle() <= 0)
        {
            winningTeam = WinningTeam.ENEMYWON;
            return;
        }

        //now based on who has less ships 
        if(enemyPD.GetNumOfPawnsInBattle() < playerPD.GetNumOfPawnsInBattle())
        {
            winningTeam = WinningTeam.PLAYERWON;
            return;
        }
        else
        {
            winningTeam = WinningTeam.ENEMYWON;
            return;
        }

        //finally we just let enemy won cause a draw is still a Loss 
        Debug.Log("LOST BECAUSE OF A DRAW");
        winningTeam = WinningTeam.ENEMYWON;

    }
    public void ShowGameOverDisplay()
    {
        gameoverDisplay.SetActive(true);
        switch (winningTeam)
        {
            case WinningTeam.PLAYERWON:
                gameoverText.text = "YEAAAA DUSKMARES BIG WWWWWWWWWWWWWWWWW";
                break;
            case WinningTeam.ENEMYWON:
                gameoverText.text = "dang... duskmares kinda mid. go luminids";
                break;
        }

    }
}

public enum WinningTeam
{
    NONE, 
    PLAYERWON, 
    ENEMYWON
}
