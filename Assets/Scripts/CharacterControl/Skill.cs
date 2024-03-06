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
    public string effect;
    public LayerMask targetLayer;
    private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // stop the system when the loop of the skill has done
        AttackEnemy();
        StopEmitting();
    }

    void StopEmitting() {
        // Check if the particle system is emitting particles
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }

    void AttackEnemy() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);

        // Iterate through colliders to find enemies
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("enemy") && canAttack) {
                if (!oneTimeAttack)
                {
                    collider.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
                    StartCoroutine(CoolDownTimer());
                }
                else {
                    attackInterval -= Time.deltaTime;
                    if (attackInterval <= 0) {
                        attackInterval = 1000;
                        collider.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
                    }
                }
            }
        }
    }
    IEnumerator CoolDownTimer() {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }
        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
