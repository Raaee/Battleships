using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// an abstract class which we will use to run the states of our games
/// </summary>
public abstract class GameState : MonoBehaviour {

    //why hide in inspector? 
    //does every state have a reason to use these variables?  if not then only use it for specific states
    //who the fk has access to the game camera?
    [HideInInspector] public StateTeam stateTeam;
    [HideInInspector] public TeamSide teamSide;
    [HideInInspector] public UnityEvent onTurnCompletion;

    [SerializeField] protected GameManager gameManager;
    public virtual void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
    public abstract void TurnComplete();
    public abstract IEnumerator WaitForSec(float time);
}
public enum StateTeam {
    NONE,
    PLAYER,
    ENEMY
}
public enum TeamSide {
    NONE,
    DUSKMARE,
    LUMINID
}
