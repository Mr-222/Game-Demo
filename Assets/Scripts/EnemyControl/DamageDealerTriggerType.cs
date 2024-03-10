using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerTriggerType : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] EnemyAttack attack;
    [SerializeField] bool destory;
    int damage;
    private void Awake()
    {
        damage = attack.attackPower;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (((1 << other.gameObject.layer) & mask) != 0) //correct layer
        {
            
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
            if (destory)
            {
                Destroy(this.gameObject);
            }
        }
    }
   
}
