using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    protected bool is_dead;

    public virtual void TakeDamage(int damage)
    {
        
    }

    protected virtual void handleDeath()
    {
        Destroy(this.gameObject);
    }
}
