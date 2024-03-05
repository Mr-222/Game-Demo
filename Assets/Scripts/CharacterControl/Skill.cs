using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string _name;
    public float radius;
    public float damage;
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
        if (!gameObject.activeSelf ||!transform.GetChild(0).gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }

    void AttackEnemy() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);

        // Iterate through colliders to find enemies
        foreach (Collider collider in colliders)
        {
            Debug.Log("hit enemy");
            // Assuming enemies have a "Health" component
            //Health enemyHealth = collider.GetComponent<Health>();
            //if (enemyHealth != null)
            //{
            //    // Deal damage or perform other actions
            //    enemyHealth.TakeDamage(10); // Example damage value
            //}
        }
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
