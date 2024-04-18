using TMPro;
using UnityEngine;

public class GameConditionManager : MonoBehaviour
{
    public static GameConditionManager Instance { get; private set; }

    public PlayerHp playerhp;
    private int enemyDeathCount = 0;
    public int deathThreshold = 10; // Set this to whatever threshold you want
    [SerializeField] GameObject wintext;
    [SerializeField] TextMeshProUGUI levelGoalText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        enemyDeathCount = 0; // Reset the count
        if (levelGoalText != null)
            levelGoalText.text = "Enemies killed: " + enemyDeathCount + "/" + deathThreshold;
    }

    private void OnEnable()
    {
        EnemyStats.onEnemyDied.AddListener(HandleEnemyDeath);
    }

    private void OnDisable()
    {
        EnemyStats.onEnemyDied.RemoveListener(HandleEnemyDeath);
    }

    private void HandleEnemyDeath()
    {
        enemyDeathCount++;
        Debug.Log(enemyDeathCount);
        if (levelGoalText != null)
            levelGoalText.text = "Enemies killed: " + enemyDeathCount + "/" + deathThreshold;
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (enemyDeathCount >= deathThreshold)
        {
            EndGame();
        }
    }
    
    private void EndGame()
    {
        // Handle game over logic here
        playerhp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
        playerhp.PlayerWin();
        Debug.Log("Game Over!");
    }
}
