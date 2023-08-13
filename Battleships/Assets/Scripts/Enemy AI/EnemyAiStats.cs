using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiStats : MonoBehaviour   {

    [SerializeField] [Range(0.15f, 0.5f)] private float percentageToHit = 0.33f;
    [SerializeField] private int amountOfRounds = 29;

    public static EnemyAiStats instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public float GetPercentageToHit() {
        return percentageToHit;
    }
    public void SetPercentageToHit(float pth) {
        percentageToHit = pth;
    }

    public int GetAmountOfRounds() {
        return amountOfRounds;
    }
    public void SetAmountOfRounds(int rounds) {
        amountOfRounds = rounds;
    }
}
