using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private bool isInAction;
    EnemyLocomotionController enemyLocomotionController;
    EnemyAnimator enemyAnimator;
    [Header("AI settings")]
    [SerializeField]
    [Tooltip("Detection Radius")]
    [Range(1f, 20f)]
    float detectRadius = 10f;
    [SerializeField]
    [Tooltip("FOV")]
    [Range(1, 360)]
     int fov;

    public float DetectionRadius { get { return detectRadius; } }
    public int FOV { get { return fov; } }
    public bool IS_IN_ACTION { get { return isInAction;  } }

    public EnemyAttack[] enemyAttacks;
    public EnemyAttack curAttack;

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
    }

    // Update is called once per frame

    private void Update()
    {
        HandleRecover();
    }

    private void FixedUpdate()
    {
        HandleAction();
    }

    private void HandleAction()
    {
        if(enemyLocomotionController.currentTarget != null)
        {
            enemyLocomotionController.distantToTarget = Vector3.Distance(enemyLocomotionController.currentTarget.transform.position, this.transform.position);
        }
        if (enemyLocomotionController.currentTarget == null)
        {
            enemyLocomotionController.HandleDetection();
        }
        else if (enemyLocomotionController.distantToTarget >= enemyLocomotionController.stoppingDistance)
        {
            enemyLocomotionController.HandleMoveToTarget();
        }
        else if(enemyLocomotionController.distantToTarget < enemyLocomotionController.stoppingDistance)
        {
            //attack
            attackTarget();
        }
    }

    private void HandleRecover()
    {
        if(currRecovery > 0)
        {
            currRecovery -= Time.deltaTime;
        }
        if (isInAction)
        {
            if(currRecovery <= 0)
            {
                isInAction = false;
            }
        }
    }

    private void attackTarget()
    {
        if (isInAction) { return; }
        if(curAttack == null)
        {
            getAttackAction();
        }
        else
        {
            isInAction = true;
            currRecovery = curAttack.Recovery;
            enemyAnimator.PlayAnimation(curAttack.actionAnimation, true);
            curAttack = null;
        }
    }

    private void getAttackAction()
    {
        Vector3 targetDir = enemyLocomotionController.currentTarget.transform.position - transform.position;
        float viewAngle = Vector3.Angle(targetDir, transform.forward);
        enemyLocomotionController.distantToTarget = Vector3.Distance(enemyLocomotionController.currentTarget.transform.position, transform.position);

        int maxPower = 0;
        for(int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttack enemyAttack = enemyAttacks[i];
            if(enemyLocomotionController.distantToTarget <= enemyAttack.maxAttackDist && enemyLocomotionController.distantToTarget >= enemyAttack.minAttackDist)
            {
                if(viewAngle <= enemyAttack.maxAttackDist && viewAngle >= enemyAttack.minimumAttackAngle)
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
            if (enemyLocomotionController.distantToTarget <= enemyAttack.maxAttackDist && enemyLocomotionController.distantToTarget >= enemyAttack.minAttackDist)
            {
                if (viewAngle <= enemyAttack.maxAttackDist && viewAngle >= enemyAttack.minimumAttackAngle)
                {
                    if(curAttack != null) { return; }

                    tmp += enemyAttack.attackPower;
                    if(tmp > randomValue)
                    {
                        curAttack = enemyAttack;
                    }

                }
            }
        }


    }



    
}
