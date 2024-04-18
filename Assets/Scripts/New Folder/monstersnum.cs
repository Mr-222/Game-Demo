using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monstersnum : MonoBehaviour
{

    public int nummax=10;
    public int num = 0;
    public PlayerHp playerhp;
    // Start is called before the first frame update
    void Start()
    {
        playerhp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (num >= nummax)
        {
            playerhp.PlayerWin();
        }
    }
}
