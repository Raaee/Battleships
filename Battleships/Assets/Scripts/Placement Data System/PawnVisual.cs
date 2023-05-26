using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO: change box collider as well when the sprite changes (pair program)
/// https://forum.unity.com/threads/changing-boxcollider2d-size-to-match-sprite-bounds-at-runtime.267964/
/// https://www.sunnyvalleystudio.com/blog/how-to-auto-adjust-sprite-collider
///
///  TODO: when objects are placed it sends its data to the pawn data (pair program)
///      //look at todo in pawn script 
///
///  TODO: stop coloring when all characters are placed. Create a "confirm" that does nothing for now
/// </summary>
public class PawnVisual : MonoBehaviour
{
   private SpriteRenderer sr;
   [SerializeField] private Sprite horizontalSprite;
   [SerializeField] private Sprite verticalSprite;
   private PotentialShipPlacement potentialShipPlacement;
    private BoxCollider2D bc2D;

   private ClickAndDrag clickAndDrag;
   
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
    private void RefreshBoxCollider() {
       // bc2D.size = new Vector2(sr.size.x, sr.size.y);

        // try double collision box enable disable, depending on the sprite that is active (bc this offset and size changing doesnt work):
        bc2D.offset = new Vector2(0, 0);
        bc2D.size = new Vector2(sr.bounds.size.x / transform.lossyScale.x, sr.bounds.size.y / transform.lossyScale.y);
    }
    
}
