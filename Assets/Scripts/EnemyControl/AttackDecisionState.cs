using UnityEngine;

public class AttackDecisionState : State
{
    [SerializeField] IdleState idleState;
    [SerializeField] PursueTargetState pursueTargetState;
    [SerializeField] AttackState attackState;

    public override State Tick(EnemyManager enemyManager, EnemyStats stats, EnemyAnimator enemyAnimator)
    {
        if (enemyManager.currentTarget == null) //target die, go back to idle
        {
            return idleState;
        }

        HandleRotateTowardsTarget(enemyManager);

        if (enemyManager.isInAction)
        {
            enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            enemyAnimator.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
        }
        enemyManager.distanceToTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.transform.position);

        if(enemyManager.currRecovery <= 0 && enemyManager.distanceToTarget <= enemyManager.maximum_attack_range)
        {
            return attackState;
        }
        else if(enemyManager.distanceToTarget > enemyManager.maximum_attack_range)
        {
            //enemyManager.isInAction = false;
            return pursueTargetState;
        }
        else
        {
            return this;
        }

    }

    private void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        if (enemyManager.isInAction)
        {
            enemyManager.navMeshAgent.updateRotation = false;
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
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
}