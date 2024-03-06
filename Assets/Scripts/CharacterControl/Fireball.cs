using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,4f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy")) {
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
