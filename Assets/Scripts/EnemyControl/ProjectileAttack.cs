using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] Projectile prefab;
    [SerializeField] Transform instantiate_point;
    EnemyManager enemyManager;
    Rigidbody tmp;
    public int timeout = 5;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    public void instanteAttack()
    {
        if (enemyManager.currentTarget != null)
        {
            Projectile clone = Instantiate(prefab, instantiate_point.position, Quaternion.identity);
            clone.timeoutDestructor = timeout;
            tmp = enemyManager.currentTarget.gameObject.GetComponent<Rigidbody>();
            //if(tmp == null)
            //{
            //    Debug.Log(tmp.velocity);
            //    return;
            //}
            clone.LaunchProjectile(enemyManager.currentTarget.transform, tmp.velocity);
        }
    }
}
