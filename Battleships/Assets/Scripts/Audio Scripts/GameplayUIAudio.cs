using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIAudio : MonoBehaviour
{

    public FMODUnity.EventReference battleHorn1Event;
    public FMODUnity.EventReference pickUpPawnEvent;
    public FMODUnity.EventReference placePawnEvent;

    public void PlayBattleHorn1()
    {
        FMODUnity.RuntimeManager.PlayOneShot(battleHorn1Event, transform.position);
    }
   
    public void PickUpPawn()
    {
        FMODUnity.RuntimeManager.PlayOneShot(pickUpPawnEvent, transform.position);
    }
    public void PlacedPawned()
    {
        FMODUnity.RuntimeManager.PlayOneShot(placePawnEvent, transform.position);
    }
}
