using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyStats))]
public class BossHP : MonoBehaviour
{
    [SerializeField] private Image hpbar;
    private EnemyStats enemyStats;
    
    private void Start()
    {
      enemyStats = GetComponent<EnemyStats>();
      hpbar.fillAmount = 1.0f;
    }
    
    private void Update()
    {
        hpbar.fillAmount = (float)enemyStats.currentHealth / enemyStats.maxHealth;
    }
}