using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    public float generatorTimer = 10f;
    float timer;

    public int enemyMax=10;
    int enemyNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateWT()
    {
        if (enemyNum < enemyMax)
        {
            Invoke("Obj", generatorTimer);
        }
    }
    void Obj()
    {
        GameObject enemyObj = Instantiate(enemy, new Vector3(Random.Range(-90, 90f), 1, Random.Range(-90, 90f)), Quaternion.identity);
        enemyNum++;
    }
}
