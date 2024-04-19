using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    [HideInInspector]public Animator anim;
    
    public void PlayAnimation(string Anim, bool is_interact)
    {
        print(is_interact);
        //anim.applyRootMotion = is_interact;
        anim.SetBool("interact", is_interact);
        anim.CrossFade(Anim, 0.2f);
    }
}
