using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : AnimationControl   {

    [SerializeField] [Range(0, 255)] protected byte enemyGeneralDimness = 127;

    public override void AnimControlStart() {
        teamSide = TeamSide.LUMINID; // DEFUALT
        DetermineSide();
        explosion = Instantiate(explosionPrefab, explosionSpawnLoc.transform.position, Quaternion.identity);
    }
    public override void AnimControlUpdate() {
       if (isAttacking) {
            float step = attackSpeed * Time.deltaTime;
            attack.transform.position = Vector3.MoveTowards(attack.transform.position, target.transform.position, step);
           
            if (attack.transform.position == target.transform.position) {
                isAttacking = false;
                attack.transform.position = attackSpawnLoc.transform.position;
                explosion.transform.position = target.position;
               // Debug.Log("EXPLOSION!!!!!");
                FindObjectOfType<CameraShake>().shakeDuration = cameraShakeDuration;
                StartCoroutine(RemoveExplosionAfterTime(1.5f));
            }
       }       
    }
    public byte GetEnemyGeneralDimness() {
        return enemyGeneralDimness;
    }
    public override void StartAttack(GameObject attackLoc) {
        //Debug.Log("Enemy Start Attack");
        target = attackLoc.transform;
        isAttacking = true;
    }
    public override void ShowTurnIndicator(StateTeam whoseTurn) {

    }
}
