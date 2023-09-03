using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    public FMODUnity.EventReference uiClickEvent;
    public FMODUnity.EventReference uiActionEvent;


    public void PlayUIClickEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot(uiClickEvent, transform.position);
    }

    public void PlayUIActionEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot(uiActionEvent, transform.position);
    }

}
