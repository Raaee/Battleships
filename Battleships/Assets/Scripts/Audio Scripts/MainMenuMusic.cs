
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    //holding place for events in FMOD 
    private FMOD.Studio.EventInstance instance;

    //the actual music event
    public FMODUnity.EventReference mainMenuEvent;

    //helper variables for functionality
    private bool isEQon = false;
    private const string MAIN_MENU_EQ_PARAM = "MainMenuEQParam";
   
    void Start()
    {
        //instantiating the music event into the instance
        instance = FMODUnity.RuntimeManager.CreateInstance(mainMenuEvent);

        //starting the event (playing the music)
        instance.start();
    }


    public void TriggerMusicEQState()
    {
        if(isEQon == false)
        {
            instance.setParameterByName(MAIN_MENU_EQ_PARAM, 1);
            isEQon = true;
        }
        else
        {
            instance.setParameterByName(MAIN_MENU_EQ_PARAM, 0);
            isEQon = false;
        }
    }
   
}
