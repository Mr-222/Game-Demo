using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyLocomotionController : MonoBehaviour
{


    EnemyManager enemyManager;
    EnemyAnimator enemyAnimator;

    [SerializeField] LayerMask detectLayer;
    public CharacterStats currentTarget;
    NavMeshAgent navMeshAgent;
    public Rigidbody enemyRB;

    public float distantToTarget;
    [Header("Locomotion Setting")]
    [Tooltip("Stopping Distance")]
    public float stoppingDistance = 1f;
    [Tooltip("Rotation Speed")]
    [SerializeField] float RotationSpeed = 20f;

    [SerializeField] float speed = 2f;
    

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<EnemyAnimator>();
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
        if (enemyManager.IS_IN_ACTION)
        {
            return;
        }
        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        float angleToTarget = Mathf.Abs(Vector3.Angle(targetDirection, transform.forward));
        distantToTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        //if we are doing something, we first need to terminate the action
        if (enemyManager.IS_IN_ACTION)
        {
            enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
        }
        else
        {
            if(distantToTarget > stoppingDistance)
            {
                enemyAnimator.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                targetDirection.Normalize();
                targetDirection.y = 0;

                //define velocity
                targetDirection *= speed;
                Vector3 projectedVelocity = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
                enemyRB.velocity = projectedVelocity;    

            }
            else
            {
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
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
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed / Time.deltaTime);
        }
        else
        {
            Vector3 reltiveDir = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
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
