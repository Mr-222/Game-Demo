
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Animator anim;
    public float maxMana;
    private float currentMana;
    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = 100; //placeholder for now
        currentHealth = maxHealth;
        currentMana = maxMana;
        anim = GetComponent<Animator>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            is_dead = true;
            handleDeath();
        }
    }

    public void ReduceMana(int mana) {
    
    }
    protected override void handleDeath()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death() {
        anim.SetTrigger("IsDead");
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponent<AttackScript>().enabled = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
