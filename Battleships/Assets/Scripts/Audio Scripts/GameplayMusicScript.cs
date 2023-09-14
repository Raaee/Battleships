using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusicScript : MonoBehaviour
{
    //holding place for events in FMOD 
    private FMOD.Studio.EventInstance gameplayMusicInst;
    
    //the actual music event
    public FMODUnity.EventReference gameplayMusicEvent;
 

    //helper variables for functionality
    private const string GAMEPLAY_AUDIO_PARAM = "StageInBattle";
    private float stageInBattle = 1.0f;
    private const float incrementStageAmt = 0.5f;


    //gameplay Data 
    GameManager gameManager;
    GameOverState gameOverState;
    private float percentage = 0.25f;


    private void Start()
    {
        gameplayMusicInst = FMODUnity.RuntimeManager.CreateInstance(gameplayMusicEvent);
        gameManager = FindObjectOfType<GameManager>();
        gameOverState = FindObjectOfType<GameOverState>();
        gameOverState.OnGameOver.AddListener(StopGameplayMusic);
        
    }

    public void IncreaseStageInBattle()
    {
        stageInBattle += incrementStageAmt;
        gameplayMusicInst.setParameterByName(GAMEPLAY_AUDIO_PARAM, stageInBattle);
        Debug.Log(stageInBattle + " is the current stage");
    }

    private void Update()
    {
        CheckPercentage();
        /*
        if(Input.GetKeyDown(KeyCode.I))
        {
            IncreaseStageInBattle();
            Debug.Log(stageInBattle + " is the current stage");
        }*/
    }

    private void CheckPercentage()
    {
       
        if (gameManager.GetStagePercentage() > 0.99f)
        {
            return;
        }

        if (gameManager.GetStagePercentage() > percentage)
        {
            Debug.Log("increasing since we passed " + percentage);
            IncreaseStageInBattle();
            percentage += 0.25f;
        }


    }

    public void StartGameplayMusic()
    {
        gameplayMusicInst.start();


        var mainMenuMusic = FindObjectOfType<MainMenuMusic>();

        if(mainMenuMusic)
            FindObjectOfType<MainMenuMusic>().StopDroneLoop();
    }

    public void StopGameplayMusic()
    {
        gameplayMusicInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
