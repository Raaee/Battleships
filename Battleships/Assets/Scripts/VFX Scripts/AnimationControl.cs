using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationControl : MonoBehaviour {

    [Header("Generals' Animation Stuff:")]
    [SerializeField] protected GameObject duskmareAttackPrefab;
    [SerializeField] protected GameObject luminidAttackPrefab;
    [SerializeField] protected GameObject hitText;
    [SerializeField] protected GameObject missText;
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected GameObject DMattackSpawnLoc;
    [SerializeField] protected GameObject LMattackSpawnLoc;
    [SerializeField] protected GameObject explosionSpawnLoc;
    [SerializeField] protected float cameraShakeDuration = 0.25f;

    [Header("Debug")]
    public GameObject attack;
    protected GameObject attackSpawnLoc;
    public float heightOfHitMissText = 1.0f;

    protected Transform target;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public TeamSide teamSide = TeamSide.NONE; // Default

    protected GameObject duskmareAttack;
    protected GameObject luminidAttack;
    protected GameObject explosion;
    protected GameObject vfxIndicator;

    [SerializeField] protected float attackSpeed = 20;

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
    public void ShowHitMissText(GameObject attackCube, bool hit) {
      
        var attackLoc = attackCube.transform.localPosition;
        Vector3 loc = new Vector3(attackLoc.x, (attackLoc.y + heightOfHitMissText), attackLoc.z);
        Debug.Log("is we hitting? " + hit); 
        if (hit)
            vfxIndicator = Instantiate(hitText, loc, Quaternion.identity);
        else
            vfxIndicator = Instantiate(missText, loc, Quaternion.identity);

        var c = FindObjectOfType<Canvas>();
        //2 ref to dummy prefabs 
        //choose a canvas serialize field \
        //make spawnd object a child of canvas 
        //move spawned obj to dummy prefabs 
        //vfx corountine destryo, figure out the exact seconds of 1 loop 
        //Destroy(vfx)
        vfxIndicator.gameObject.transform.parent = c.gameObject.transform;
        Debug.Log("this canvas", c.gameObject);

        if (vfxIndicator == null)
            Debug.Log("rae is fired");

    }

    public abstract void AnimControlStart();
    public abstract void AnimControlUpdate();
    public abstract void StartAttack(GameObject attackLoc);
}
