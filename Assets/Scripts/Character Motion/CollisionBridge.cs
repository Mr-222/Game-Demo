using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBridge : MonoBehaviour
{
    private HashSet<CharacterStats> enemy_set;
    private List<CharacterStats> toBeRemoved = new List<CharacterStats>();

    private void Update()
    {
        toBeRemoved.Clear();
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach(CharacterStats s in enemy_set)
            {
                if(s != null)
                    s.TakeDamage(25);
                else
                {
                    toBeRemoved.Add(s);
                }
            }

        }
        foreach(CharacterStats s in toBeRemoved)
        {
            remove_enemy(s);
        }
    }

    private void Start()
    {
        enemy_set = new HashSet<CharacterStats>();
    }

    public void add_enemy(CharacterStats obj)
    {
        enemy_set.Add(obj);
    }

    public void remove_enemy(CharacterStats obj)
    {
        enemy_set.Remove(obj);
    }

   
}
