using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float minSpeed = 10; // Speed of the projectile
    public float maxSpeed = 20;
    float speed;
    public Rigidbody rb;
    public int timeoutDestructor;


    private IEnumerator StartDestructionTimer()
    {
        yield return new WaitForSeconds(timeoutDestructor);
        Destroy(gameObject);
    }

    // Launch the projectile towards the target
    public void LaunchProjectile(Transform target, Vector3 targetVelocity)
    {
        Vector3 toTarget = target.position - transform.position; // Distance to the target
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0;

        // Time it will take for the projectile to reach the target if it's stationary
        float timeToTarget = toTargetXZ.magnitude / speed;

        // Predict the future position of the target
        Vector3 predictedTargetPosition = target.position + targetVelocity * timeToTarget;

        // Aim towards the predicted position
        Vector3 direction = (predictedTargetPosition - transform.position).normalized;
        
        direction.y = 0; //may need to change in the future
        //print(direction);
        rb.velocity = direction * speed;

        StartCoroutine(StartDestructionTimer());
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Projectile script requires a Rigidbody component.");
        }
        speed = Random.Range(minSpeed, maxSpeed);
    }

}
