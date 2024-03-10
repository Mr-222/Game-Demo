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
    public Collider o;

    private void OnTriggerEnter(Collider other)
    {

        o = other;
        if (((1 << other.gameObject.layer) & mask) != 0) //correct layer
        {
            
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);

        }
        print(other.gameObject);
        Destroy(this.gameObject);
    }
   
}
