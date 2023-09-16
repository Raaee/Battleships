using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIAudio : MonoBehaviour
{

    public FMODUnity.EventReference battleHorn1Event;
    public FMODUnity.EventReference battleHornLossEvent;
    public FMODUnity.EventReference battleHornWinEvent;


    public FMODUnity.EventReference pickUpPawnEvent;
    public FMODUnity.EventReference placePawnEvent;
    public FMODUnity.EventReference playerTurnEvent;

    public FMODUnity.EventReference clickSquareEvent;
    private FMOD.Studio.EventInstance clickSquareInstance;
    private const string CLICK_SQUARE_PAN_PARAM = "ClickSquarePanParam";
    private GameOverState gameOverState;

    private void Start()
    {
        gameOverState = FindObjectOfType<GameOverState>();
        gameOverState.OnGameLoss.AddListener(PlayBattleHornLoss);
        gameOverState.OnGameWin.AddListener(PlayBattleHornWin);

        clickSquareInstance = FMODUnity.RuntimeManager.CreateInstance(clickSquareEvent);
    }

    public void PlayClickSquare(float xPos)
    {


        float panPos = ((xPos / 9) * 1);
      
        clickSquareInstance.setParameterByName(CLICK_SQUARE_PAN_PARAM, panPos);

        clickSquareInstance.start();
    }
    public void PlayBattleHorn1()
    {
        FMODUnity.RuntimeManager.PlayOneShot(battleHorn1Event, transform.position);
    }

    public void PlayBattleHornLoss()
    {
        FMODUnity.RuntimeManager.PlayOneShot(battleHornLossEvent, transform.position);
    }

    public void PlayBattleHornWin()
    {
        FMODUnity.RuntimeManager.PlayOneShot(battleHornWinEvent, transform.position);
    }


    public void PickUpPawn()
    {
        FMODUnity.RuntimeManager.PlayOneShot(pickUpPawnEvent, transform.position);
    }
    public void PlacedPawned()
    {
        FMODUnity.RuntimeManager.PlayOneShot(placePawnEvent, transform.position);
    }

    public void PlayPlayerTurn()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerTurnEvent, transform.position);
    }
}
