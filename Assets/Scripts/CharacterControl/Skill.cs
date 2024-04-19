using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent onSkillDestroy = new UnityEvent();

    [SerializeField] private GameObject skillVFX;
    private List<GameObject> spawnedIceVFXs = new List<GameObject>();
    
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
            Destroy(gameObject);
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
                    var enemyStats = collider.gameObject.GetComponent<EnemyStats>();
                    onSkillDestroy.AddListener(enemyStats.StartResetStatusCoroutine);
                    enemyStats.TakeDamage(damage);
                    if (_name == "Snow")
                    {
                        if ((enemyStats.status & EnemyStats.Status.Freeze) != EnemyStats.Status.Freeze)
                        {
                            var iceVfx = Instantiate(skillVFX, 
                                collider.transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
                            iceVfx.transform.localScale = new Vector3(3, 3, 3);
                            spawnedIceVFXs.Add(iceVfx);
                        }
                        enemyStats.status |= EnemyStats.Status.Freeze;
                    }
                    if (_name == "Meteors")
                    {
                        enemyStats.status |= EnemyStats.Status.Burned;
                        EnemyManager enemyManager = collider.gameObject.GetComponent<EnemyManager>();
                        if (enemyManager.burnVfx == null)
                        {
                            enemyManager.burnVfx = Instantiate(skillVFX,
                                enemyManager.transform.position + new Vector3(0f, .2f, 0), Quaternion.identity);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnDestroy()
    {
        onSkillDestroy?.Invoke();
        foreach (var vfx in spawnedIceVFXs)
            Destroy(vfx);
    }
}
