
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Animator anim;
    public float maxMana;
    private float currentMana;
    PlayerHp playerHp;
    
    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = 100; //placeholder for now
        currentHealth = maxHealth;
        currentMana = maxMana;
        anim = GetComponent<Animator>();
        playerHp = GetComponent<PlayerHp>();
    }
    

    public override void TakeDamage(int damage)
    {
        anim.SetTrigger("GetHit");
        currentHealth -= damage;
        playerHp.Addhp(-damage);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            is_dead = true;
            handleDeath();
        }
    }

    public void UpdateMana(float mana)
    {
            currentMana = currentMana + mana;
            playerHp.Addmp(mana);
    }
    protected override void handleDeath()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death() {
        anim.SetTrigger("IsDead");
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponent<AttackScript>().enabled = false;
        this.enabled = false;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
