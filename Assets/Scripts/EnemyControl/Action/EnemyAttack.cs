using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy AI Action")]
public class EnemyAttack : EnemyAction
{
    public int attackPower = 3;
    public float Recovery = 2;

    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;

    public float minAttackDist = 0;
    public float maxAttackDist = 3;
}
