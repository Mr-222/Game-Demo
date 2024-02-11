using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private bool isInAction;
    EnemyLocomotionController enemyLocomotionController;
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
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        HandleDetection();
    }

    private void HandleDetection()
    {
        if (enemyLocomotionController.currentTarget == null)
        {
            enemyLocomotionController.HandleDetection();
        }
        else
        {
            enemyLocomotionController.HandleMoveToTarget();
        }
    }

    
}
