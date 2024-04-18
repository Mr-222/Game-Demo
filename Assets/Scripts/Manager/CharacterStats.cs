using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] public int maxHealth;
    public int currentHealth;
    protected bool is_dead;

    public virtual void TakeDamage(int damage)
    {
        Debug.Log("taking Damage");
    }

    protected virtual void handleDeath()
    {
        Destroy(this.gameObject);
    }
}
