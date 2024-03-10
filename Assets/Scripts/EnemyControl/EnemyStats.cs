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

        if(enemyManager.CURRENT_STATE == idleState)
        {
            enemyManager.set_max_detectRadius();
            enemyManager.set_max_fov();
        }
        
        currentHealth -= damage;
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

    public void Die()
    {
        onEnemyDied?.Invoke();
        Destroy(this.gameObject);
    }
}

