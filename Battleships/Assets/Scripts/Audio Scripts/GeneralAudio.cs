using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAudio : MonoBehaviour
{
    public FMODUnity.EventReference groundShakeEvent;
    public FMODUnity.EventReference playSetupConfirmedEvent;

    private FMOD.Studio.EventInstance groundShakeInstance;
    private const String GROUND_SHAKE_PAN_PARAM = "GroundShakePanParam";


    private void Awake()
    {
        groundShakeInstance = FMODUnity.RuntimeManager.CreateInstance(groundShakeEvent);
    }
    public void PlayGroundShake(float xPos, bool isPlayerSide)
    {
        float panPos;
        if (isPlayerSide)
        {
             panPos = ((xPos / 9) * 1) - 1;
        }
        else
        {
            panPos = ((xPos / 9) * 1);
        }


        Debug.Log("Pan pos is " + -panPos);
        groundShakeInstance.setParameterByName(GROUND_SHAKE_PAN_PARAM, -panPos);
        groundShakeInstance.start();
    }

    public void PlaySetupConfirmed()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playSetupConfirmedEvent, transform.position);
    }
}
