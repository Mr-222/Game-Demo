using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyLocomotionController : MonoBehaviour
{


    EnemyManager enemyManager;

    [SerializeField] LayerMask detectLayer;
    public CharacterStats currentTarget;
    NavMeshAgent navMeshAgent;
    Rigidbody enemyRB;

    public float distantToTarget;
    [Header("Locomotion Setting")]
    [Tooltip("Stopping Distance")]
    [SerializeField] float stoppingDistance = 1f;
    [Tooltip("Rotation Speed")]
    [SerializeField] float RotationSpeed = 20f;

    [SerializeField] float speed = 2f;
    

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMeshAgent.enabled = false;
        enemyRB.isKinematic = false;
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.DetectionRadius, detectLayer);
        for(int i =0; i < colliders.Length; i++)
        {
            CharacterStats targetCharStats = colliders[i].transform.GetComponent<CharacterStats>();
            if(targetCharStats != null)
            {

            Vector3 directionToTarget = targetCharStats.transform.position - transform.position;
            float angleToTarget = Mathf.Abs(Vector3.Angle(directionToTarget, transform.forward));
            //The target is within our detection
            if(angleToTarget <= enemyManager.FOV / 2)
                {
                    currentTarget = targetCharStats; //lock on
                }
               
            }
        }
    }

    public void HandleMoveToTarget()
    {
        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        float angleToTarget = Mathf.Abs(Vector3.Angle(targetDirection, transform.forward));
        distantToTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        //if we are doing something, we first need to terminate the action
        if (enemyManager.IS_IN_ACTION)
        {
            navMeshAgent.enabled = false;
        }
        else
        {
            if(distantToTarget > stoppingDistance)
            {
                targetDirection.Normalize();
                targetDirection.y = 0;

                //define velocity
                targetDirection *= speed;
                Vector3 projectedVelocity = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
                enemyRB.velocity = projectedVelocity;    

            }
            else
            {

            }

        }
        HandleRotateTowardsTarget();
        navMeshAgent.nextPosition = transform.position;
        navMeshAgent.transform.localRotation = Quaternion.identity;

    }

    private void HandleRotateTowardsTarget()
    {
        if (enemyManager.IS_IN_ACTION)
        {

        }
        else
        {
            Vector3 chaseVelocity = enemyRB.velocity;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(currentTarget.transform.position);
            enemyRB.velocity = chaseVelocity;
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
        }
        
    }

}
