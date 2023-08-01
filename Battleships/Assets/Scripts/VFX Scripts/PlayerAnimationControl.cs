using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : AnimationControl   {
    
    public override void AnimControlStart() {
        teamSide = TeamSide.DUSKMARE; // DEFAULT
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
                FindObjectOfType<CameraShake>().shakeDuration = cameraShakeDuration;
                //Debug.Log("EXPLOSION!!!!!");
                StartCoroutine(RemoveExplosionAfterTime(1.5f));
            }
       }       
    }

    public override void StartAttack(GameObject attackLoc) {
       // Debug.Log("Player Start Attack");
        target = attackLoc.transform;
        isAttacking = true;
    }
    

}
