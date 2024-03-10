using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    public int damage;
    private void OnCollisionEnter(Collision other)
    {
        if (((1 << other.gameObject.layer) & mask) != 0) //correct layer
        {
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
