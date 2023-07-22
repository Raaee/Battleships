using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// an abstract class which we will use to run the states of our games
/// </summary>
public abstract class GameState : MonoBehaviour {

    [HideInInspector] public StateTeam stateTeam;
    [HideInInspector] public UnityEvent onTurnCompletion;

    [SerializeField] protected GameManager gameManager;
    protected virtual void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
    public abstract void TurnComplete();

}
public enum StateTeam {
    NONE,
    PLAYER,
    ENEMY
}
