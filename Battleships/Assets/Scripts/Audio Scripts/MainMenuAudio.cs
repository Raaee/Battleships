using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAudio : MonoBehaviour
{
    public FMODUnity.EventReference uiClickEvent;
    public FMODUnity.EventReference uiActionEvent;


    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private FMOD.Studio.VCA musicVca;
    private FMOD.Studio.VCA sfxVCA;

    private const string musicVCAID = "MusicVCA";
    private const string sfxVCAID = "SFXVCA";
    private const string vcaPrefix = "vca:/";

    private void Start()
    {
        musicVca = FMODUnity.RuntimeManager.GetVCA(vcaPrefix +musicVCAID);
        sfxVCA = FMODUnity.RuntimeManager.GetVCA(vcaPrefix + sfxVCAID);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
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

}
