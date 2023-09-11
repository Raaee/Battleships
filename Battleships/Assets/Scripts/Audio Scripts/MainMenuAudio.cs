using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAudio : MonoBehaviour
{
    public FMODUnity.EventReference uiClickEvent;
    public FMODUnity.EventReference uiActionEvent;
    public FMODUnity.EventReference fireSnuffEvent;
    public FMODUnity.EventReference fireLoopEvent;
    public FMODUnity.EventReference loadingScreenStingEvent;
    public FMODUnity.EventReference diffFireLoopEvent;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private FMOD.Studio.VCA musicVca;
    private FMOD.Studio.VCA sfxVCA;

    private FMOD.Studio.EventInstance fireLoopInstance;
    private FMOD.Studio.EventInstance diffFireLoopInstance;


    private const string DIFF_FIRE_LOOP_EQ_PARAM = "DiffFireEQParam";
    private const string musicVCAID = "MusicVCA";
    private const string sfxVCAID = "SFXVCA";
    private const string vcaPrefix = "vca:/";

    private void Start()
    {
        musicVca = FMODUnity.RuntimeManager.GetVCA(vcaPrefix +musicVCAID);
        sfxVCA = FMODUnity.RuntimeManager.GetVCA(vcaPrefix + sfxVCAID);

        fireLoopInstance = FMODUnity.RuntimeManager.CreateInstance(fireLoopEvent);
        diffFireLoopInstance = FMODUnity.RuntimeManager.CreateInstance(diffFireLoopEvent);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        StartFireLoopEvent();
    }

    public void PlayDiffFire(float buttonNumber)
    {
        diffFireLoopInstance.setParameterByName(DIFF_FIRE_LOOP_EQ_PARAM, buttonNumber);
        diffFireLoopInstance.start();
    }

    public void StopDiffFire()
    {
        diffFireLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT );
    }

    public void PlayFireSnuffEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot(fireSnuffEvent, transform.position);
    }

    private void StartFireLoopEvent()
    {
        fireLoopInstance.start();
    }

    public void StopFireLoopEvent()
    {
        fireLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void SetSFXVolume(float newSfxVolume)
    {
        sfxVCA.setVolume(newSfxVolume);
    }

    private void SetMusicVolume(float newMusicVol)
    {
        musicVca.setVolume(newMusicVol);
    }

    public void PlayUIClickEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot(uiClickEvent, transform.position);
    }

    public void PlayUIActionEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot(uiActionEvent, transform.position);
    }

    public void PlayLoadingScreenStinger()
    {
        FMODUnity.RuntimeManager.PlayOneShot(loadingScreenStingEvent, transform.position);
      
    }

}
