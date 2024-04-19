using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Animator anim;
    EnemyManager enemyManager;
    [SerializeField] IdleState idleState;
    public UIEnemyHealthBar enemyHealthBar;
    public static UnityEvent onEnemyDied = new UnityEvent();
    
    [Flags]
    public enum Status
    {
        Normal = 0x0,
        Freeze = 0x1,
        Burned = 0x2,
    }
    public Status status;
    public bool isBurning;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyManager = GetComponent<EnemyManager>();
        status = Status.Normal;
        isBurning = false;
    }
    
    void Start()
    {
        //maxHealth = 100; //placeholder for now
        currentHealth = maxHealth;
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        if (is_dead)
        {
            return;
        }

        if(enemyManager.CURRENT_STATE == idleState)
        {
            enemyManager.set_max_detectRadius();
            enemyManager.set_max_fov();
        }
        if (currentHealth > 0) {
            currentHealth -= damage;
        }
        if (!enemyManager.IS_IN_ACTION)
        {
            anim.Play("TakeDamage");
        }
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

    public bool iswin;
    public void Die()
    {
        onEnemyDied?.Invoke();
        if (iswin)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>().PlayerWin();
        }
        // GameObject.FindGameObjectWithTag("monstersnum").GetComponent<monstersnum>().num++;
        
        Destroy(this.gameObject);
    }
    
    private IEnumerator ResetStatus()
    {
        // Cancel freeze status
        status &= ~Status.Freeze;
        yield return new WaitForSeconds(5f);
        status = Status.Normal;
    }
    
    public void StartResetStatusCoroutine()
    {
        StartCoroutine(ResetStatus());
    }
}

