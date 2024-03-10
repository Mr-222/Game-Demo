using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public bool isInAction;
    EnemyLocomotionController enemyLocomotionController;
    EnemyAnimator enemyAnimator;
    EnemyStats enemyStats;
    [Header("AI settings")]
    [SerializeField]
    [Tooltip("Detection Radius")]
    [Range(1f, 20f)]
    float detectRadius = 10f;
    [SerializeField]
    [Tooltip("FOV")]
    [Range(1, 360)]
     int fov;

    [SerializeField] private State currentState;

    public float DetectionRadius { get { return detectRadius; } }
    public int FOV { get { return fov; } }
    public bool IS_IN_ACTION { get { return isInAction;  } }

    public CharacterStats currentTarget;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    


    public float distanceToTarget;
    public float viewAngle;

    [Header("Locomotion Settings")]
    [Tooltip("maximum attack range")]
    public float maximum_attack_range = 1.5f;
    [Tooltip("Rotation Speed")]
    [SerializeField] float rotationSpeed = 20f;


    public float currRecovery = 0; 

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.transform.position, detectRadius);

        Vector3 forward = transform.forward;
        Quaternion leftRotation = Quaternion.AngleAxis(-fov / 2, Vector3.up);
        Quaternion rightRotation = Quaternion.AngleAxis(fov / 2, Vector3.up);

        Vector3 leftDirection = leftRotation * forward;
        Vector3 rightDirection = rightRotation * forward;

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * detectRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * detectRadius);
    }

    // Start is called before the first frame update
    void Awake()
    {
        enemyLocomotionController = GetComponent<EnemyLocomotionController>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyStats = GetComponent<EnemyStats>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = maximum_attack_range;

    }

    // Update is called once per frame

    private void Update()
    {
        HandleRecover();
        //HandleStateMachine();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if(currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimator);
            if (nextState != null) {
                SwitchState(nextState);
            }
        }



        //if (enemyLocomotionController.currentTarget == null)
        //{
        //    enemyLocomotionController.HandleDetection();
        //}
        //else if (enemyLocomotionController.distanceToTarget >= enemyLocomotionController.stoppingDistance)
        //{
        //    Debug.Log("what");
        //    enemyLocomotionController.HandleMoveToTarget();
        //}


        //else if(enemyLocomotionController.distanceToTarget < enemyLocomotionController.stoppingDistance)
        //{
        //    //attack
        //    enemyLocomotionController.navMeshAgent.isStopped = true;
        //    enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        //    Debug.Log("what2");
        //    Debug.Log("??????");
        //    //attackTarget();
        //    Debug.Log("isInAction");
        //}
    }

    private void SwitchState(State state)
    {
        currentState = state;
    }

    private void HandleRecover()
    {
        if (currRecovery > 0)
        {
            currRecovery -= Time.deltaTime;
        }
        if (isInAction)
        {
            if (currRecovery <= 0)
            {
                isInAction = false;
            }
        }
    }

    //private void attackTarget()
    //{
    //    Debug.Log("isInAction" + isInAction);

    //    if (isInAction) { return; }
    //    if(curAttack == null)
    //    {
    //        getAttackAction();
    //    }
    //    else
    //    {

    //        isInAction = true;
    //        currRecovery = curAttack.Recovery;
    //        enemyAnimator.PlayAnimation(curAttack.actionAnimation, true);
    //        curAttack = null;
    //    }
    //}

    //private void getAttackAction()
    //{
    //    Vector3 targetDir = enemyLocomotionController.currentTarget.transform.position - transform.position;
    //    float viewAngle = Vector3.Angle(targetDir, transform.forward);
    //    enemyLocomotionController.distanceToTarget = Vector3.Distance(enemyLocomotionController.currentTarget.transform.position, transform.position);

    //    int maxPower = 0;
    //    for(int i = 0; i < enemyAttacks.Length; i++)
    //    {
    //        EnemyAttack enemyAttack = enemyAttacks[i];
    //        if(enemyLocomotionController.distanceToTarget <= enemyAttack.maxAttackDist && enemyLocomotionController.distanceToTarget >= enemyAttack.minAttackDist)
    //        {
    //            if(viewAngle <= enemyAttack.maxAttackDist && viewAngle >= enemyAttack.minimumAttackAngle)
    //            {
    //                maxPower += enemyAttack.attackPower;
    //            }
    //        }
    //    }
    //    int randomValue = Random.Range(0, maxPower);
    //    int tmp = 0;

    //    for (int i = 0; i < enemyAttacks.Length; i++)
    //    {
    //        EnemyAttack enemyAttack = enemyAttacks[i];
    //        if (enemyLocomotionController.distanceToTarget <= enemyAttack.maxAttackDist && enemyLocomotionController.distanceToTarget >= enemyAttack.minAttackDist)
    //        {
    //            if (viewAngle <= enemyAttack.maxAttackDist && viewAngle >= enemyAttack.minimumAttackAngle)
    //            {
    //                if(curAttack != null) { return; }

    //                tmp += enemyAttack.attackPower;
    //                if(tmp > randomValue)
    //                {
    //                    curAttack = enemyAttack;
    //                }

    //            }
    //        }
    //    }


    //}




}
