using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour   {
    [SerializeField] GameObject duskmareAttackPrefab;
    [SerializeField] GameObject luminidAttackPrefab;
    [SerializeField] GameObject attackSpawnLoc;
    private GameObject attack;
    public GameObject randomDebugObj;

    [SerializeField] private Transform target;
    [SerializeField] private float speed = 110;
    private bool isAttacking = false;

    private GameObject duskmareSpawn;

    private void Start() {
        duskmareSpawn = Instantiate(duskmareAttackPrefab, attackSpawnLoc.transform.position, Quaternion.identity);
    }
    void Update() {
     
       // if (isAttacking) {
            float step = speed * Time.deltaTime;
        duskmareSpawn.transform.position = Vector3.MoveTowards(duskmareSpawn.transform.position, randomDebugObj.transform.position, step);
       
        // }
        /*  if (attack.transform.position == target.transform.position) {
              isAttacking = false;
              attack.transform.position = attackSpawnLoc.transform.position;
              Debug.Log("EXPLOSION!!!!!");
         }*/
    }

    public void StartAttack(GameObject attackLoc, StateTeam team) {
        target = randomDebugObj.transform;
        if (team == StateTeam.PLAYER) {
            attack = duskmareAttackPrefab;
        } else if (team == StateTeam.ENEMY) {
            attack = luminidAttackPrefab;
        }
        isAttacking = true;
    }
   
}
//Pete waS HERE 