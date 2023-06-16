using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState initialState;
    [Header("Debug")]
    [SerializeField] private GameState currentState;

    private void Awake() {
        currentState = initialState;
        initialState.OnStateEnter();
    }

    private void Update() {
        currentState.OnStateUpdate();
    }

    public void ChangeState(GameState newState) {
        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }
}
