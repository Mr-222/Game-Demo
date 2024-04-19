using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerTriggerStayType : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] bool destroy;
    public int damage = 5;
    private Dictionary<GameObject, float> lastDamageTime = new Dictionary<GameObject, float>();
    private float damageInterval = 1.0f;  // Interval in seconds between damage applications



    public void setDamage(int D)
    {

        Debug.Log("gooooo");

        damage = D;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCorrectLayer(other.gameObject))
        {
            // Initialize last damage time for new entries
            if (!lastDamageTime.ContainsKey(other.gameObject))
            {
                lastDamageTime[other.gameObject] = Time.time - damageInterval;  // Allows immediate first damage
            }
        }

        if (destroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsCorrectLayer(other.gameObject))
        {
            if (CanDamageAgain(other.gameObject))
            {
                other.gameObject.GetComponent<CharacterStats>()?.TakeDamage(damage);
                lastDamageTime[other.gameObject] = Time.time;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lastDamageTime.ContainsKey(other.gameObject))
        {
            lastDamageTime.Remove(other.gameObject);
        }
    }

    private bool IsCorrectLayer(GameObject obj)
    {
        return ((1 << obj.layer) & mask) != 0;
    }

    private bool CanDamageAgain(GameObject obj)
    {
        return Time.time >= lastDamageTime[obj] + damageInterval;
    }

}
