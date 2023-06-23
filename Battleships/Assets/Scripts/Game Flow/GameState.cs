using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// an abstract class which we will use to run the states of our games
/// </summary>
public abstract class GameState : MonoBehaviour
{
    protected GameManager gameManager;
    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();


}
