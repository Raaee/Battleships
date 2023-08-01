using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The base pawn script, for data purposes and visually when seeting their spawn positions 
/// </summary>
public class Pawn : MonoBehaviour {
    //size 
    [SerializeField] [Range(1, 5)] private int pawnSize = 1;

    //position - list of PawnCoordinates 
    public List<Vector2> pawnCoords;

   // private ClickAndDrag cd;
    private PotentialShipPlacement potentialShipPlacement;
    private GridManager gridMan;
    private bool placed = false;

    [Header("shake values!")]
    private float shakeDuration = 1f;
    private float maxShakeAmount = 0.1f;
    private float shakeSpeed = 0.1f;

    private Vector3 originalPosition;

    private PlayerAnimationControl playerAnimationControl;
    private bool canDoFeedback = true;
    private bool isBadGuy = false; //dumb script
    private void Awake()
    {
        //cd = GetComponent<ClickAndDrag>();
       // cd.OnPawnPlaced.AddListener(SetPawnCoordinates);
        potentialShipPlacement = FindObjectOfType<PotentialShipPlacement>();
        gridMan = FindObjectOfType<GridManager>();
        playerAnimationControl = FindObjectOfType<PlayerAnimationControl>();
        playerAnimationControl.OnMissileDestroyed.AddListener(KillPawnFeedback);
    }

    public bool SetPawnCoordinates()
    {
        List<GameObject> lastHighlightedGameobjects = potentialShipPlacement.GetLastHighlightedObjects();

        if (lastHighlightedGameobjects == null)
        {
            Debug.Log("theres no highlighted gameobjects dummy. most likely on the wrong grid");
            return false;
        }
        
        //TODO: convert the gameobjects into the specific pawncords
        pawnCoords.Clear();
        for (int i = 0; i < lastHighlightedGameobjects.Count; i++) {
            pawnCoords.Add(gridMan.GetPositionAtTile(lastHighlightedGameobjects[i]));
        }
        return true;
    }
    public void SetPawnCoordinates(List<Vector2> newPawnCoords) {
        pawnCoords.Clear();
        pawnCoords = newPawnCoords;
        
        //TODO: Go through all the cubes with these pawn coords, and set them to occupied 
        //next: if hit/miss set their state like that too 
    }
    public void RemovePawnCoord(Vector2 coordToRemove) {
        //coordToRemove = new Vector2(coordToRemove.y, coordToRemove.x);

        if (pawnCoords.Contains(coordToRemove)) {
            Debug.Log("removing a pawn coord");
            pawnCoords.Remove(coordToRemove);
            if(pawnCoords.Count <= 0)
            {
                Debug.Log("Pawn: TIME TO DIE!!!! Enemy");
                gameObject.SetActive(false);//my guy its already set to false in game 
                //KillPawnFeedback();
            }
        } else {
            Debug.Log("Pawn coord not found: " + coordToRemove);
        }
    }

    public void SetBadGuy()
    {
        isBadGuy = true;
    }

    private void KillPawnFeedback()
    {
        if (!canDoFeedback) return;
        if (pawnCoords.Count > 0) return;
        if (isBadGuy == false) return;
        //save original pos
        originalPosition = transform.position;
        //we show it 
        gameObject.SetActive(true);
        //shake for impact for x seconds
        StartCoroutine(ShakeCoroutine());
        canDoFeedback = false;
    }

    IEnumerator ShakeCoroutine()
    {
        float timeElapsed = 0f;

        while (timeElapsed < shakeDuration)
        {
            float xShake = UnityEngine.Random.Range(-maxShakeAmount, maxShakeAmount);
            float yShake = UnityEngine.Random.Range(-maxShakeAmount, maxShakeAmount);

            transform.position = originalPosition + new Vector3(xShake, yShake, 0f);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Reset the position to the original after shaking is done
        transform.position = originalPosition;
        gameObject.SetActive(false);
    }

    public int GetPawnSize() {
        return pawnSize;
    }
    public void SetPlacedStatus(bool isPlaced) {
        placed = isPlaced;
    }
    public bool GetPlacedStatus() {
        return placed;
    }
}
//create kill feedback