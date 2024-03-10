using System.Collections;
using System.Collections.Generic;
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
