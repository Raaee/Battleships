using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO: change box collider as well when the sprite changes (pair program?)
/// </summary>
public class PawnVisual : MonoBehaviour
{
   private SpriteRenderer sr;
   [SerializeField] private Sprite horizontalSprite;
   [SerializeField] private Sprite verticalSprite;
   private PotentialShipPlacement potentialShipPlacement;

   private ClickAndDrag clickAndDrag;
   
   private void Awake()
   {
      sr = GetComponent<SpriteRenderer>();
      clickAndDrag = GetComponent<ClickAndDrag>();
      potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
      potentialShipPlacement.OnMouseScrolled.AddListener(ChangePawnVisual);
   }

   

   private void ChangePawnVisual()
   {
      if (!clickAndDrag.GetIsDragging()) return;

      Debug.Log("changin pawn visual");
      sr.sprite = potentialShipPlacement.GetPawnOrientation() == PawnOrientation.HORIZONTAL ? horizontalSprite : verticalSprite;

    
     
   }
}
