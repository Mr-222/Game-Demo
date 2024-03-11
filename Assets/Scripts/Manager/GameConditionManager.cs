using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConditionManager : MonoBehaviour
{
    public static GameConditionManager Instance { get; private set; }

    private int enemyDeathCount = 0;
    public int deathThreshold = 10; // Set this to whatever threshold you want
    [SerializeField] GameObject wintext;

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
        wintext.SetActive(true);
        Debug.Log("Game Over!");

    }


}
