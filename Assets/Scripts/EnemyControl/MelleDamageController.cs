using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleDamageController : MonoBehaviour
{
    [SerializeField] AttackState attackState;
    [SerializeField] DamageDealerTriggerType[] damageControllers;

    // Start is called before the first frame update
    void Start()
    {
        if (attackState == null || damageControllers == null)
        {
            Debug.Log("unitilaized");
        }
    }

    // Update is called once per frame
    public void SetDamageForMelleWeapon(int index)
    {
        EnemyAttack tmp = attackState.CURRENT_ATTACK;
        if(tmp != null && index < damageControllers.Length)
        {
            damageControllers[index].setDamage(tmp.attackPower);
        }
    }
}
