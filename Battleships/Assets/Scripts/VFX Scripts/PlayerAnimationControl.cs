using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationControl : AnimationControl   {

    
    [SerializeField] [Range(0, 255)] protected byte enemyGeneralDimness = 127;
    
    [Header("Events")]
    public UnityEvent OnMissileDestroyed;
    private GeneralAudio generalAudio;
    private GameObject cubeObj;
    private void Awake()
    {
        generalAudio = FindObjectOfType<GeneralAudio>();
    }
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
                var enemyGridManager = FindObjectOfType<BetterEnemyPlacement>().GetEnemyGridManager();
                Vector2 cubeV2 = enemyGridManager.GetPositionAtTile(cubeObj);



                generalAudio.PlayGroundShake(cubeV2.x, true);
                OnMissileDestroyed?.Invoke();
                //Debug.Log("EXPLOSION!!!!!");
                StartCoroutine(RemoveExplosionAfterTime(1.5f));
            }
       }       
    }
    public byte GetEnemyGeneralDimness() {
        return enemyGeneralDimness;
    }

    public override void StartAttack(GameObject attackLoc) {
       // Debug.Log("Player Start Attack");
        target = attackLoc.transform;
        isAttacking = true;
        cubeObj = attackLoc;
        generalAudio.PlayDuskmareAttack();

    }
    public override void ShowTurnIndicator(StateTeam whoseTurn) {
    
    }


}
