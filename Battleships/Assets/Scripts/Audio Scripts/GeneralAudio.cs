using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAudio : MonoBehaviour
{
    public FMODUnity.EventReference groundShakeEvent;
    public FMODUnity.EventReference playSetupConfirmedEvent;

    public void PlayGroundShake()
    {
        FMODUnity.RuntimeManager.PlayOneShot(groundShakeEvent, transform.position);
    }

    public void PlaySetupConfirmed()
    {
        FMODUnity.RuntimeManager.PlayOneShot(groundShakeEvent, transform.position);
    }
}
