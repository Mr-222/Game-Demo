using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    public float hp = 10;
    float hpMax;
    public Image image;
    EnemyGenerator enemyGenerator;
    // Start is called before the first frame update
    void Start()
    {
        hpMax = hp;
        enemyGenerator = GameObject.FindGameObjectWithTag("mng").GetComponent<EnemyGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = hp / hpMax;
        if (hp <= 0)
        {
            enemyGenerator.GenerateWT();
            Destroy(this.gameObject);
        }
    }
}
