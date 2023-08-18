using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIAnimationControl : AnimationControl  {

    [Header("Hit and Miss Animation")]
    [SerializeField] protected Canvas gameCanvas;
    [SerializeField] protected GameObject hitText;
    [SerializeField] protected GameObject missText;
    [SerializeField] protected GameObject DM_FBIndicatorLoc;
    [SerializeField] protected GameObject LM_FBIndicatorLoc;
    public float heightOfHitMissText = 5.0f;

    [Header("Turn Indicators")]
    [SerializeField] protected GameObject turnPanelPrefab;
    [SerializeField] protected GameObject turnPanelSpawnLoc;
    [SerializeField] protected TMP_Text turnIndicatorText;
    [SerializeField] protected CanvasGroup duskmareGeneral;
    [SerializeField] protected CanvasGroup luminidGeneral;

    protected GameObject turnIndicator;

    public override void AnimControlStart() {
        turnIndicatorText = turnPanelPrefab.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void AnimControlUpdate() {

    }


    public void ShowHitMissText(GameObject attackCube, bool hit, TeamSide teamSide) {
        var attackLoc = attackCube.transform.localPosition;
        Vector3 loc = new Vector3(attackLoc.x, (attackLoc.y + heightOfHitMissText), attackLoc.z);

        if (hit)
            vfxIndicator = Instantiate(hitText, gameCanvas.transform, worldPositionStays: false);

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
            Debug.Log("vfx indicator is null.");
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
    public override void ShowTurnIndicator(StateTeam whoseTurn) {
        switch (whoseTurn) {
            case StateTeam.PLAYER:
                turnIndicatorText.text = "playeR turN";
                break;
            case StateTeam.ENEMY:
                turnIndicatorText.text = "ENemy tUrN";
                break;
        }
        turnIndicator = Instantiate(turnPanelPrefab, gameCanvas.transform, worldPositionStays: false);
        turnIndicator.transform.position = turnPanelSpawnLoc.transform.position;
        //Debug.Log("turn indicator");
    }

    public override void StartAttack(GameObject attackLoc) {
        
    }
}
