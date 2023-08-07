using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  The Pawn script is a class that represents a game object in a grid 
///  The script manages pawn properties such as size, position, and coordinates, as well as its placed status on the grid. 
///  Additionally, the script includes a shake animation for visual feedback when a pawn is destroyed.
/// </summary>
public class Pawn : MonoBehaviour {

    #region Variables
  
    [SerializeField] [Range(1, 5)] private int pawnSize = 1;
    
    public List<Vector2> pawnCoords;
    private bool placed = false;

    //pawn object shake values
    private float shakeDuration = 1f;
    private float maxShakeAmount = 0.1f;
    private Vector3 originalPosition;

    private PotentialShipPlacement potentialShipPlacement;
    private GridManager gridMan;
    private PlayerAnimationControl playerAnimationControl;

    private bool canDoFeedback = true;
    private bool isBadGuy = false;
    #endregion

    private void Awake()
    {
       
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
        Debug.Log("we placing");
       

        return true;
    }
    public void SetPawnCoordinates(List<Vector2> newPawnCoords) {
        pawnCoords.Clear();
        pawnCoords = newPawnCoords;
        
        
    }
    public void RemovePawnCoord(Vector2 coordToRemove) {

        if (pawnCoords.Contains(coordToRemove)) {
            Debug.Log("removing a pawn coord");
            pawnCoords.Remove(coordToRemove);
            if(pawnCoords.Count <= 0)
            {
                Debug.Log("Pawn: TIME TO DIE!!!! Enemy");
                gameObject.SetActive(false);//my guy its already set to false in game 
               
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
        if(placed == false)
        {
            ResetPawnCoords();
        }
    }
    public bool GetPlacedStatus() {
        return placed;
    }
    public void ResetPawnCoords()
    {
        pawnCoords = new List<Vector2>();
    }
}
//create kill feedback