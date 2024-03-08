using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
    private float timer = 0.0f;
    public float changeInterval = 30.0f;

    void Start() {
    	Vector3 newPosition = new Vector3(Random.Range(xMin, xMax), transform.position.y, Random.Range(zMin, zMax));
        Debug.Log("This" + newPosition);
        transform.position = newPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            ChangePosition();
        }
    }

    void ChangePosition()
    {
        Vector3 newPosition = new Vector3(Random.Range(xMin, xMax), transform.position.y, Random.Range(zMin, zMax));
        transform.position = newPosition;
    }
}
