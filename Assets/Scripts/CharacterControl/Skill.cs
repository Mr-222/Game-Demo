using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float attackInterval= 0f;
    public bool canAttack;
    public bool oneTimeAttack;
    public string _name;
    public float radius;
    public int damage;
    public int manaCost;
    public string effect;
    public LayerMask targetLayer;
    
    // Update is called once per frame
    void Update()
    {
        // stop the system when the loop of the skill has done
        if (canAttack)
        {
            AttackEnemy();
        }
        StopEmitting();
    }

    void StopEmitting() {
        // Check if the particle system is emitting particles
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }

    void AttackEnemy()
    {
        StartCoroutine(AttackWithInterval());

    }
    IEnumerator AttackWithInterval()
    {
        canAttack = false;
        // Iterate through colliders to find enemies
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("enemy"))
                {
                    if (!oneTimeAttack)
                    {
                        collider.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
                    }
                    else
                    {
                        collider.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
                    }
                }
            }
            yield return new WaitForSeconds(attackInterval);
        }

        canAttack = true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
