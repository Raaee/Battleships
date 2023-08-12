using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationControl : MonoBehaviour {

    [Header("Generals' Animation")]
    [SerializeField] protected GameObject duskmareAttackPrefab;
    [SerializeField] protected GameObject luminidAttackPrefab;
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected GameObject DMattackSpawnLoc;
    [SerializeField] protected GameObject LMattackSpawnLoc;
    [SerializeField] protected GameObject explosionSpawnLoc;
    [SerializeField] protected float cameraShakeDuration = 0.25f;
    [SerializeField] protected float attackSpeed = 20;

    [Header("Debug")]
    public GameObject attack;
    protected GameObject attackSpawnLoc;

    protected Transform target;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public TeamSide teamSide = TeamSide.NONE; // Default

    protected GameObject duskmareAttack;
    protected GameObject luminidAttack;
    protected GameObject explosion;
    protected GameObject vfxIndicator;

    private void Update() {
        AnimControlUpdate();
    }
    private void Start() {
        AnimControlStart();
    }
    public void DetermineSide() {
        if (teamSide == TeamSide.DUSKMARE) {
            duskmareAttack = Instantiate(duskmareAttackPrefab, DMattackSpawnLoc.transform.position, Quaternion.identity);
            attack = duskmareAttack;
            attackSpawnLoc = DMattackSpawnLoc;
        }
        else if (teamSide == TeamSide.LUMINID) {
            luminidAttack = Instantiate(luminidAttackPrefab, LMattackSpawnLoc.transform.position, Quaternion.identity);
            attack = luminidAttack;
            attackSpawnLoc = LMattackSpawnLoc;
        }
    }
    public IEnumerator RemoveExplosionAfterTime(float time) {
        yield return PeteHelper.GetWait(time);
       
        explosion.transform.position = explosionSpawnLoc.transform.position;
       // Debug.Log("RESTING EXPLOSION");
    }       
    public abstract void AnimControlStart();
    public abstract void AnimControlUpdate();
    public abstract void StartAttack(GameObject attackLoc);
    public abstract void ShowTurnIndicator(StateTeam whoseTurn);
}
