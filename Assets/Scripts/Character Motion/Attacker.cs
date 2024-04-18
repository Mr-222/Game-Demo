using UnityEngine;

public class Attacker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            transform.parent.GetComponent<CollisionBridge>().add_enemy(other.GetComponent<CharacterStats>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            transform.parent.GetComponent<CollisionBridge>().remove_enemy(other.GetComponent<CharacterStats>());
        }
    }
}
