using UnityEngine;

public class EnemyAnimator : AnimatorManager
{

    EnemyLocomotionController enemyLocomotionController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyLocomotionController = GetComponentInParent<EnemyLocomotionController>();
    }
}
