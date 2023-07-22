using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour   {
    [SerializeField] GameObject duskmareAttackPrefab;
    [SerializeField] GameObject luminidAttackPrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject DMattackSpawnLoc;
    [SerializeField] GameObject LMattackSpawnLoc;
    [SerializeField] GameObject explosionSpawnLoc;
    private GameObject attack;
    private GameObject attackSpawnLoc;

    private Transform target;
    [HideInInspector] public bool isAttacking = false;

    private GameObject duskmareAttack;
    private GameObject luminidAttack;
    private GameObject explosion;

    [SerializeField] private float attackSpeed = 20;


    private void Start() {
        duskmareAttack = Instantiate(duskmareAttackPrefab, DMattackSpawnLoc.transform.position, Quaternion.identity);
        luminidAttack = Instantiate(luminidAttackPrefab, LMattackSpawnLoc.transform.position, Quaternion.identity);
        explosion = Instantiate(explosionPrefab, explosionSpawnLoc.transform.position, Quaternion.identity);
    }
    void Update() {
       if (isAttacking) {
            float step = attackSpeed * Time.deltaTime;
            attack.transform.position = Vector3.MoveTowards(attack.transform.position, target.transform.position, step);
           
            if (attack.transform.position == target.transform.position) {
                isAttacking = false;
                attack.transform.position = attackSpawnLoc.transform.position;
                explosion.transform.position = target.position;
                Debug.Log("EXPLOSION!!!!!");
                StartCoroutine(RemoveExplosionAfterTime(1.5f));
            }
       }       
    }

    public void StartAttack(GameObject attackLoc, StateTeam team) {
        target = attackLoc.transform;
        Debug.Log(team);
        if (team == StateTeam.PLAYER) {
            attack = duskmareAttack;
            attackSpawnLoc = DMattackSpawnLoc;
        } else if (team == StateTeam.ENEMY) {
            attack = luminidAttack;
            attackSpawnLoc = LMattackSpawnLoc;
        }
        isAttacking = true;
    }
    private IEnumerator RemoveExplosionAfterTime(float time) {
        yield return PeteHelper.GetWait(time); 
        explosion.transform.position = explosionSpawnLoc.transform.position;
        Debug.Log("RESTING EXPLOSION");
    }

}
//Pete waS HERE 