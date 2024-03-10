using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] LayerMask detectLayer;
    [SerializeField] PursueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyStats stats, EnemyAnimator enemyAnimator)
    {
        //handle detection
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.DetectionRadius, detectLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats targetCharStats = colliders[i].transform.GetComponent<CharacterStats>();
            if (targetCharStats != null)
            {
                Vector3 directionToTarget = targetCharStats.transform.position - transform.position;
                float angleToTarget = Mathf.Abs(Vector3.Angle(directionToTarget, transform.forward));
                // The target is within our detection
                if (angleToTarget <= enemyManager.FOV / 2)
                {
                    enemyManager.currentTarget = targetCharStats; // Lock on
                }
            }
        }

        //switch state
        if(enemyManager.currentTarget != null)
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }
}
 