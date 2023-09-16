using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAudio : MonoBehaviour
{
    public FMODUnity.EventReference groundShakeEvent;
    public FMODUnity.EventReference playSetupConfirmedEvent;

    public FMODUnity.EventReference duskmareAttkEvent;
    public FMODUnity.EventReference luminidAttkEvent;

    private FMOD.Studio.EventInstance groundShakeInstance;
    private const String GROUND_SHAKE_PAN_PARAM = "GroundShakePanParam";


    private void Awake()
    {
        groundShakeInstance = FMODUnity.RuntimeManager.CreateInstance(groundShakeEvent);
    }
    public void PlayGroundShake(float xPos, bool isPlayerSide)
    {
        float panPos = ((xPos / 9) * 1);


        if (isPlayerSide)
        {
            
            //do nothing
          
        }
        else
        {
            panPos -= 1;
            panPos *= -1;
        }


      
        groundShakeInstance.setParameterByName(GROUND_SHAKE_PAN_PARAM, panPos);
        groundShakeInstance.start();
    }

    public void PlayDuskmareAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShot(duskmareAttkEvent, transform.position);
    }

    public void PlayLuminidAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShot(luminidAttkEvent, transform.position);
    }

    public void PlaySetupConfirmed()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playSetupConfirmedEvent, transform.position);
    }
}
