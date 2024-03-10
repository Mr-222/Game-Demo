using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Animator anim;
    EnemyManager enemyManager;
    public UIEnemyHealthBar enemyHealthBar;
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyManager = GetComponent<EnemyManager>();
    }
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
        if (is_dead)
        {
            return;
        }
        
        currentHealth -= damage;
        anim.Play("TakeDamage");
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
        enemyManager.navMeshAgent.enabled = false;
        anim.Play("Die");
        //Destroy(this.gameObject);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}

