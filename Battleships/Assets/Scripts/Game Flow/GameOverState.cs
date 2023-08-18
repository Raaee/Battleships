using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class checks who won and shows the respective data for winning 
/// </summary>
public class GameOverState : GameState
{

    [SerializeField] private BetterEnemyPlacement enemyPD;
    [SerializeField] private PlayerPlacementData playerPD;

    [Header("Diplay")]
    [SerializeField] private GameObject gameoverCanvas;
    [SerializeField] private CanvasGroup gameoverPanel;
    [SerializeField] private GameObject header;
    [SerializeField] private Color32 winColor;
    [SerializeField] private Color32 loseColor;
    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private TextMeshProUGUI headerText;

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
    public override IEnumerator CompleteTurnDelay(float time) {
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
    }
    public void ShowGameOverDisplay()
    {
        gameoverCanvas.SetActive(true);
        switch (winningTeam)
        {
            case WinningTeam.PLAYERWON:
                gameoverText.text += "\n YEaaaa DUSKmARES BIG W";
                headerText.text = "You Won!";
                header.GetComponent<Image>().color = winColor;
                break;
            case WinningTeam.ENEMYWON:
                gameoverText.text += "\n  go luminids";
                headerText.text = "You Lost.";
                header.GetComponent<Image>().color = loseColor;
                break;
        }

    }
    public void ShowEnemyLocation() {
        gameoverPanel.alpha = 0;
        // show enemy loc
        foreach(GameObject pawn in enemyPD.GetEnemiesToShow()) {
            pawn.SetActive(true);
        }
    }
    
    public void UnShowEnemyLoc() {
        gameoverPanel.alpha = 1;
        // unshow enemy loc
        foreach (GameObject pawn in enemyPD.GetEnemiesToShow()) {
            pawn.SetActive(false);
        }
    }
}

public enum WinningTeam
{
    NONE, 
    PLAYERWON, 
    ENEMYWON
}
