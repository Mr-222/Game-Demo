using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{

    [SerializeField] IdleState idleState;
    [SerializeField] AttackDecisionState attackDecisionState;

    public override State Tick(EnemyManager enemyManager, EnemyStats stats, EnemyAnimator enemyAnimator)
    {

        if (enemyManager.currentTarget == null) //target die, go back to idle
        {
            return idleState;
        }

        if (enemyManager.IS_IN_ACTION) // we have a target and is attacking -> stop go to AttackDecisionState
        {
            enemyManager.navMeshAgent.isStopped = true;
            enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return attackDecisionState;
        }


        //Debug.Log("dist:" + distanceToTarget);

        enemyManager.distanceToTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        if (enemyManager.distanceToTarget > enemyManager.maximum_attack_range)
        {
            // If the distance to the target is greater than the stopping distance, move towards the target
            enemyManager.navMeshAgent.isStopped = false; // Ensure the NavMeshAgent is not stopped
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position); // Set the destination to the target's position

            // Update the animator to reflect the movement
            // Assuming "Vertical" represents forward movement in your animator parameters
            //enemyAnimator.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            HandleMovement(enemyManager, enemyAnimator);

            return this; // Stay in the current state
        }
        else
        {
            //// If within stopping distance, might transition to an attack state or something similar
            //// Placeholder for further logic you might want to add here
            //enemyManager.navMeshAgent.isStopped = true; // Stop moving
            //enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime); // Stop walking/running animation

            //// Possible transition to another state, such as preparing to attack
            //// return someState;
            return attackDecisionState;
        }
    }

    void HandleMovement(EnemyManager enemyManager, EnemyAnimator enemyAnimator)
    {
        Vector3 normalizedMovement = enemyManager.navMeshAgent.desiredVelocity.normalized;

        Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward);
        //Debug.Log("forward" + forwardVector);

        Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);

        float forwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, transform.forward);

        float rightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, transform.right);

        enemyAnimator.anim.SetFloat("Vertical", Mathf.InverseLerp(-1f, 1f, forwardVelocity), 0.1f, Time.deltaTime);
        enemyAnimator.anim.SetFloat("Horizontal", Mathf.InverseLerp(-1f, 1f, rightVelocity), 0.1f, Time.deltaTime);
    }
}
