using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// pawn visual for vert or hori. will also have the animations and stuff 
/// </summary>
public class PawnVisual : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Sprite horizontalSprite;
    [SerializeField] private Sprite verticalSprite;
    private PotentialShipPlacement potentialShipPlacement;
    private BoxCollider2D bc2D;

    private ClickAndDrag clickAndDrag;
    private PawnOrientation pawnOrientation = PawnOrientation.HORIZONTAL;
    private void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        clickAndDrag = GetComponent<ClickAndDrag>();
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        potentialShipPlacement.OnMouseScrolled.AddListener(ChangePawnVisual);
    }
    private void ChangePawnVisual()
    {
        if (!clickAndDrag.GetIsDragging()) return;
        sr.sprite = potentialShipPlacement.GetPawnOrientation() == PawnOrientation.HORIZONTAL ? horizontalSprite : verticalSprite;
        RefreshBoxCollider();
    }
    //Pete: this isnt working right now :)
    private void RefreshBoxCollider() {
        // bc2D.size = new Vector2(sr.size.x, sr.size.y);

        // try double collision box enable disable, depending on the sprite that is active (bc this offset and size changing doesnt work):
        bc2D.offset = new Vector2(0, 0);
        bc2D.size = new Vector2(sr.bounds.size.x / transform.lossyScale.x, sr.bounds.size.y / transform.lossyScale.y);
    }
    public void ChangePawnVisual(PawnOrientation pawnOrientation) {
        if (pawnOrientation == PawnOrientation.VERTICAL)
        {
            this.pawnOrientation = pawnOrientation;
            sr.sprite = verticalSprite;
        } else {
            this.pawnOrientation = pawnOrientation;
            sr.sprite = horizontalSprite;
        }
    }

    public PawnOrientation GetPawnOrientation()
    {
        return pawnOrientation;
    }
}
