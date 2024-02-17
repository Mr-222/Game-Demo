using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBridge : MonoBehaviour
{
    private HashSet<HPScript> enemy_set;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach(HPScript s in enemy_set)
            {
                s.hp -= 5;
            }

        }
    }

    private void Start()
    {
        enemy_set = new HashSet<HPScript>();
    }

    public void add_enemy(HPScript obj)
    {
        enemy_set.Add(obj);
    }

    public void remove_enemy(HPScript obj)
    {
        enemy_set.Remove(obj);
    }

   
}
