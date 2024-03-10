using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ResetInteract : StateMachineBehaviour
{
    public string targetParameter;
    public bool status;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(targetParameter);
        animator.SetBool(targetParameter, status);
    }
}
