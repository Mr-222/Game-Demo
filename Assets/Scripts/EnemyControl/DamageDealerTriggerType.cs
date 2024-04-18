using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerTriggerType : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] bool destory;
    int damage = 5;



    public void setDamage(int D) {

        Debug.Log("gooooo");

        damage = D;
    }
  
    private void OnTriggerEnter(Collider other)
    {

 
        if (((1 << other.gameObject.layer) & mask) != 0) //correct layer
        {
            
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
            //GetComponent<Collider>().enabled = false; //turn off

        }
        if (destory)
        {
            Destroy(this.gameObject);
        }
        
    }
   
}
