using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour   {
    [SerializeField] GameObject duskmareAttack;
    [SerializeField] GameObject luminidAttack;
    [SerializeField] GameObject attackSpawnLoc;
    private GameObject attack;
    public GameObject at;

    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1;
    private bool isAttacking = false;

    private void Start() {
        Instantiate(duskmareAttack, attackSpawnLoc.transform.position, Quaternion.identity);
      //  Instantiate(luminidAttack, attackSpawnLoc.transform.position, Quaternion.identity);
    }
    void Update() {
       // if (isAttacking) {
            float step = speed * Time.deltaTime;
        Debug.Log(step);
        duskmareAttack.transform.position = Vector3.MoveTowards(attackSpawnLoc.transform.position, at.transform.position, step);
        Debug.Log(duskmareAttack.transform.position);
       // }
      /*  if (attack.transform.position == target.transform.position) {
            isAttacking = false;
            attack.transform.position = attackSpawnLoc.transform.position;
            Debug.Log("EXPLOSION!!!!!");
       }*/
    }

    public void StartAttack(GameObject attackLoc, StateTeam team) {
        Debug.Log("Starting Attack!");
        target = at.transform;
        if (team == StateTeam.PLAYER) {
            attack = duskmareAttack;
        } else if (team == StateTeam.ENEMY) {
            attack = luminidAttack;
        }
        isAttacking = true;
    }
   
}
