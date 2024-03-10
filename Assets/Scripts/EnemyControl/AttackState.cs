using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    public EnemyAttack[] enemyAttacks;
    [SerializeField] EnemyAttack curAttack;
    [SerializeField] AttackDecisionState attackDecisionState;
    [SerializeField] IdleState idleState;



    public override State Tick(EnemyManager enemyManager, EnemyStats stats, EnemyAnimator enemyAnimator)
    {
        if(enemyManager.currentTarget == null)
        {
            curAttack = null;
            return idleState;
        }


        HandleRotateTowardsTarget(enemyManager);

        if (enemyManager.IS_IN_ACTION)
        {
            return attackDecisionState;
        }

        
        if(curAttack != null)
        {
            //Debug.Log(enemyManager.distanceToTarget < curAttack.maxAttackDist);
            if (enemyManager.distanceToTarget < curAttack.minAttackDist)
            {
                return this;
            }
            else if(enemyManager.distanceToTarget < curAttack.maxAttackDist)
            {
                if(enemyManager.viewAngle <= curAttack.maximumAttackAngle && enemyManager.viewAngle >= curAttack.minimumAttackAngle)
                {
                    if(enemyManager.currRecovery <= 0 && !enemyManager.IS_IN_ACTION)
                    {
                        //enemyManager.navMeshAgent.
                        enemyManager.navMeshAgent.isStopped = true;
                        enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimator.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                        //enemyManager.gameObject.transform.LookAt(enemyManager.currentTarget.transform);
                        enemyAnimator.PlayAnimation(curAttack.actionAnimation, true);
                        enemyManager.isInAction = true;
                        enemyManager.currRecovery = curAttack.Recovery;
                        curAttack = null;
                        return attackDecisionState;
                    }
                }
            }
        }
        else
        {
            getAttackAction(enemyManager);
        }

        return attackDecisionState;

    }


    private void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        if (enemyManager.isInAction)
        {
            enemyManager.navMeshAgent.updateRotation = false;
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = enemyManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime); ;
        }
        else
        {
            enemyManager.navMeshAgent.updateRotation = true;
        }
    }


    private void getAttackAction(EnemyManager enemyManager)
    {
        Vector3 targetDir = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        enemyManager.viewAngle = Vector3.Angle(targetDir, transform.forward);
        enemyManager.distanceToTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        int maxPower = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttack enemyAttack = enemyAttacks[i];
            //Debug.Log("1" + (enemyManager.distanceToTarget <= enemyAttack.maxAttackDist && enemyManager.distanceToTarget >= enemyAttack.minAttackDist));
            //Debug.Log(enemyManager.viewAngle <= enemyAttack.maximumAttackAngle && enemyManager.viewAngle >= enemyAttack.minimumAttackAngle);
            
            if (enemyManager.distanceToTarget <= enemyAttack.maxAttackDist && enemyManager.distanceToTarget >= enemyAttack.minAttackDist)
            {
                if (enemyManager.viewAngle <= enemyAttack.maximumAttackAngle && enemyManager.viewAngle >= enemyAttack.minimumAttackAngle)
                {
                    maxPower += enemyAttack.attackPower;
                }
            }
        }
        int randomValue = Random.Range(0, maxPower);
        int tmp = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttack enemyAttack = enemyAttacks[i];
            if (enemyManager.distanceToTarget <= enemyAttack.maxAttackDist && enemyManager.distanceToTarget >= enemyAttack.minAttackDist)
            {
                if (enemyManager.viewAngle <= enemyAttack.maximumAttackAngle && enemyManager.viewAngle >= enemyAttack.minimumAttackAngle)
                {
                    if (curAttack != null) { return; }

                    tmp += enemyAttack.attackPower;
                    if (tmp > randomValue)
                    {
                        curAttack = enemyAttack;
                    }

                }
            }
        }


    }
}
