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

    [Header("Hit and Miss Animation")]
    [SerializeField] protected Canvas gameCanvas;
    [SerializeField] protected GameObject hitText;
    [SerializeField] protected GameObject missText;
    [SerializeField] protected GameObject DM_FBIndicatorLoc;
    [SerializeField] protected GameObject LM_FBIndicatorLoc;
    [SerializeField] protected float attackSpeed = 20;
    public float heightOfHitMissText = 5.0f;

    [Header("Debug")]
    public GameObject attack;
    protected GameObject attackSpawnLoc;

    [Header("Turn Indicators")]
    [SerializeField] protected CanvasGroup duskmareGeneral;
    [SerializeField] protected CanvasGroup luminidGeneral;
    [SerializeField] protected GameObject playerTurnPanel;
    [SerializeField] protected GameObject enemyTurnPanel;

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
    public void ShowHitMissText(GameObject attackCube, bool hit, TeamSide teamSide) {
        var attackLoc = attackCube.transform.localPosition;
        Vector3 loc = new Vector3(attackLoc.x, (attackLoc.y + heightOfHitMissText), attackLoc.z);

        if (hit)
           vfxIndicator = Instantiate(hitText, gameCanvas.transform, worldPositionStays:false);

        else
           vfxIndicator = Instantiate(missText, gameCanvas.transform, worldPositionStays: false);

        if (teamSide == TeamSide.DUSKMARE)
            vfxIndicator.transform.position = DM_FBIndicatorLoc.transform.position;
        else
            vfxIndicator.transform.position = LM_FBIndicatorLoc.transform.position;

        //2 ref to dummy prefabs 
        //choose a canvas serialize field \
        //make spawnd object a child of canvas 
        //move spawned obj to dummy prefabs 
        //vfx corountine destryo, figure out the exact seconds of 1 loop 
        //Destroy(vfx)
        // vfxIndicator.gameObject.transform.parent = gameCanvas.gameObject.transform;

        if (vfxIndicator == null)
            Debug.Log("rae is fired");

    }
    public void IndicateWhoseTurn(TeamSide whoseTurn, byte generalDimness) {
        Color32 DM_normalAlpha = duskmareGeneral.GetComponent<SpriteRenderer>().color;
        DM_normalAlpha = new Color32(DM_normalAlpha.r, DM_normalAlpha.g, DM_normalAlpha.b, 255);
        Color32 DM_lowAlpha = new Color32(DM_normalAlpha.r, DM_normalAlpha.g, DM_normalAlpha.b, generalDimness);

        Color32 LM_normalAlpha = luminidGeneral.GetComponent<SpriteRenderer>().color;
        LM_normalAlpha = new Color32(LM_normalAlpha.r, LM_normalAlpha.g, LM_normalAlpha.b, 255);
        Color32 LM_lowAlpha = new Color32(LM_normalAlpha.r, LM_normalAlpha.g, LM_normalAlpha.b, generalDimness);

        Debug.Log("DM: " + DM_lowAlpha);
        Debug.Log("LM: " + LM_lowAlpha);

        switch (whoseTurn) {
            case TeamSide.DUSKMARE:
                duskmareGeneral.GetComponent<SpriteRenderer>().color = DM_normalAlpha;
                luminidGeneral.GetComponent<SpriteRenderer>().color = LM_lowAlpha;
                break;
            case TeamSide.LUMINID:
                luminidGeneral.GetComponent<SpriteRenderer>().color = LM_normalAlpha;
                duskmareGeneral.GetComponent<SpriteRenderer>().color = DM_lowAlpha;
                break;
        }
    }

    public abstract void AnimControlStart();
    public abstract void AnimControlUpdate();
    public abstract void StartAttack(GameObject attackLoc);
}
