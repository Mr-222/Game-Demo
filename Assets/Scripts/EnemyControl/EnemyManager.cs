using System.Collections;
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
    [Range(1f, 150f)]
    float detectRadius = 10f;
    [SerializeField]
    [Tooltip("FOV")]
    [Range(1, 360)]
    int fov;

    public void set_max_detectRadius() {
        detectRadius = 150f;
    }

    public void set_max_fov()
    {
        fov = 360;
    }

    [SerializeField] private State currentState;

    public float DetectionRadius { get { return detectRadius; } }
    public int FOV { get { return fov; } }
    public bool IS_IN_ACTION { get { return isInAction;  } }
    public State CURRENT_STATE { get { return currentState; } }

    public CharacterStats currentTarget;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    
    public float distanceToTarget;
    public float viewAngle;

    [Header("Locomotion Settings")]
    [Tooltip("maximum attack range")]
    public float maximum_attack_range = 1.5f;
    [Tooltip("Rotation Speed")]
    public float rotationSpeed = 20f;
    
    public float currRecovery = 0;

    [HideInInspector] 
    public GameObject burnVfx = null;

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
        if (burnVfx != null)
            burnVfx.transform.position = this.transform.position;
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
        HandleStatus();
    }

    private void HandleStatus()
    {
        if (enemyStats.is_dead)
        {
            navMeshAgent.enabled = false;
            enemyAnimator.anim.speed = 1f;
            return;
        }
        
        if ((enemyStats.status & EnemyStats.Status.Freeze) == EnemyStats.Status.Freeze)
        {
            navMeshAgent.enabled = false;
            enemyAnimator.anim.speed = 0;
        }

        if ((enemyStats.status & EnemyStats.Status.Burned) == EnemyStats.Status.Burned && !enemyStats.isBurning)
        {
            enemyStats.isBurning = true;
            StartCoroutine(burning());
        }

        if (!enemyStats.isBurning && burnVfx != null)
        {
            Destroy(burnVfx);
            burnVfx = null;
        }
        
        if (enemyStats.status == EnemyStats.Status.Normal)
        {
            navMeshAgent.enabled = true;
            enemyAnimator.anim.speed = 1f;
        }
    }

    private IEnumerator burning()
    {
        enemyStats.TakeDamage(2);
        yield return new WaitForSeconds(1f);
        enemyStats.isBurning = false;
    }

    private void HandleStateMachine()
    {
        if(currentState != null && navMeshAgent.enabled)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimator);
            if (nextState != null) {
                SwitchState(nextState);
            }
        }
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
    
    public void disableAgentWhenAttacked()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    public void resumeAgentAfterAttack()
    {
        if (navMeshAgent != null)
            navMeshAgent.isStopped = false;
    }

    private void OnDestroy()
    {
        if (burnVfx != null)
            Destroy(burnVfx);
    }
}
