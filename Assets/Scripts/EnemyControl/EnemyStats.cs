using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public UIEnemyHealthBar enemyHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = 100; //placeholder for now
        currentHealth = maxHealth;
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public override void TakeDamage(int damage)
    {
        Debug.Log("Enemy  took damage");
        currentHealth -= damage;
        enemyHealthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            is_dead = true;
            handleDeath();
        }
    }

    protected override void handleDeath()
    {
        Destroy(this.gameObject);
    }
}

