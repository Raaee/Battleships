using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// This shows the status of the player/enemy placement positions of pawns 
/// </summary>
public class PlayerPlacementData : MonoBehaviour
{
    //an array of pawns
    public List<GameObject> pawnPrefabs; // this is the default pawn prefabs used to assign the player's army
    public List<GameObject> pawnsInBattle; // this will be the player's pawns

    public GameObject initialCoords;
    public List<GameObject> pawnSpawnLocations;

    private int ranNum;
    private bool allPawnsPlaced = false;
    private bool placementConfirmed = false;
    private int numPawnsInBattle = 4;

    public UnityEvent OnAllPawnsSpawned;
    [SerializeField] private ButtonFunctions buttonFunctions;

    public SizePalleteSO sizePalleteSO;


    private InputData inputData;
    private void Awake()
    {
        if (buttonFunctions == null)
            Debug.Log("didnt assign button funcitont in inspector dummy");
        buttonFunctions.OnPlayerConfirmPlacement.AddListener(Placed);
        inputData = FindObjectOfType<InputData>();
    }

    public void StartPlacement() {

        if (sizePalleteSO != null)
        {

            ChooseSpecificPawns();
            SpawnInitialPawns();
            return;
        }

        if (pawnPrefabs.Count < 4) {
            Debug.Log("pawn prefab list (Player) must have 4 elements.");
            return;
        } else {
            ChooseRandomPawns(numPawnsInBattle);
        }
    }

    private void ChooseSpecificPawns()
    {
        if (sizePalleteSO.sizeOfPawns.Count <= 3)
        {
            Debug.Log("the size pallete is supposed to have 4 elements bruh");
            Debug.Break();

        }
        foreach (SizePawnEnum spe in sizePalleteSO.sizeOfPawns)
        {
            GameObject pawn;
            switch (spe)
            {
                case SizePawnEnum.ONE:
                    pawn = Instantiate(PawnPrefabOfSize(1), initialCoords.transform.position, Quaternion.identity);
                    pawnsInBattle.Add(pawn);
                    break;
                case SizePawnEnum.TWO:
                    pawn = Instantiate(PawnPrefabOfSize(2), initialCoords.transform.position, Quaternion.identity);
                    pawnsInBattle.Add(pawn);
                    break;
                case SizePawnEnum.THREE:
                    pawn = Instantiate(PawnPrefabOfSize(3), initialCoords.transform.position, Quaternion.identity);
                    pawnsInBattle.Add(pawn);
                    break;
                case SizePawnEnum.FOUR:
                    pawn = Instantiate(PawnPrefabOfSize(4), initialCoords.transform.position, Quaternion.identity);
                    pawnsInBattle.Add(pawn);
                    break;
            }

           
        }
    }

        public void ChooseRandomPawns(int numPawns) {
            for (int i = 0; i < numPawns; i++) {
                ranNum = Random.Range(1, 5); // random number 1, 2, 3, or 4
                var pawn = Instantiate(PawnPrefabOfSize(ranNum), initialCoords.transform.position, Quaternion.identity);
                pawnsInBattle.Add(pawn);

                inputData.AddSelfToClickDragList(pawn.gameObject.GetComponent<ClickAndDrag>());
            }
            SpawnInitialPawns();
        }

        private GameObject PawnPrefabOfSize(int size) {
            for (int i = 0; i < pawnPrefabs.Count; i++) {
                if (pawnPrefabs[i].GetComponent<Pawn>().GetPawnSize() == size) {
                    return pawnPrefabs[i];
                }
            }
            return null;
        }

        private void Update()
        {
            CheckPawnPlacement();

        }

        // checks if there is a false place status in each pawn:
        public void CheckPawnPlacement() {
            foreach (GameObject p in pawnsInBattle) {
                if (p.GetComponent<Pawn>().GetPlacedStatus() == false) {

                    allPawnsPlaced = false;
                    return;
                }
            }
            if (pawnsInBattle.Count == 0) {
                //  Debug.Log("Nu UH");
                allPawnsPlaced = false;
            }
            else {
                allPawnsPlaced = true;
            }
        }

        public void SpawnInitialPawns() {
            foreach (GameObject p in pawnsInBattle) {
                switch (p.GetComponent<Pawn>().GetPawnSize()) {
                    case 1:
                        p.transform.position = pawnSpawnLocations[0].transform.position;
                        break;
                    case 2:
                        p.transform.position = pawnSpawnLocations[1].transform.position;
                        break;
                    case 3:
                        p.transform.position = pawnSpawnLocations[2].transform.position;
                        break;
                    case 4:
                        p.transform.position = pawnSpawnLocations[3].transform.position;
                        break;
                }
            }
            OnAllPawnsSpawned?.Invoke();
        }
        private void Placed() {
            placementConfirmed = true;
        }

        public bool GetIsAllPawnsPlaced()
        {
            return allPawnsPlaced;
        }

        public bool GetIsPlacementConfirmed()
        {
            return placementConfirmed;
        }



        //we are checking if the attack locattion matches up with a currently alive pawn coordinate, it will also remove the coordinate at the same time 
        public bool CheckIfHit(Vector2 attackLoc) {
            bool hit = false;
            Vector2 correctLoc = new Vector2(attackLoc.x, attackLoc.y);

            for (int i = 0; i < pawnsInBattle.Count; i++) {
                Pawn pawn = pawnsInBattle[i].GetComponent<Pawn>();
                for (int n = 0; n < pawn.pawnCoords.Count; n++) {
                    Vector2 pawnCoord = pawn.pawnCoords[n];
                    if (pawnCoord == correctLoc) {
                        hit = true;
                        pawn.pawnCoords.Remove(pawnCoord);
                        if (pawn.pawnCoords.Count <= 0)
                        {
                            Debug.Log("other pawn: YOLO");

                            pawnsInBattle.Remove(pawn.gameObject);
                            pawn.gameObject.SetActive(false);
                        }
                        break;
                    }
                }
            }
            if (hit)
                return true;
            else
                return false;
        }

        public void ResetPlayerPawnPlacement()
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm == null)
            {
                Debug.Log("no game manager in scene dummy");
                return;
            }

            if (gm.GetCurrentState() != gm.GetInitialState())
            {
                Debug.Log("bruh we cannot reset pawns, if youve already started the game");
                return;
            }

            foreach (GameObject pawn in pawnsInBattle)
            {
                Destroy(pawn.gameObject);
            }

            pawnsInBattle = new List<GameObject>();
            StartPlacement();
        }

        public int GetNumOfPawnsInBattle()
        {
            return pawnsInBattle.Count;
        }

    }


