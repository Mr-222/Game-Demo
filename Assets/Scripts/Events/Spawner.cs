using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabsList;
    public Transform spawnPoint; // The point where you want to spawn the prefab
    [SerializeField] int count = 5; // Total number of prefabs to spawn
    [SerializeField] float spawnInterval = 5.0f; // Time in seconds between each spawn

    private void Start()
    {
        StartCoroutine(SpawnPrefab());
    }

    private IEnumerator SpawnPrefab()
    {
        // Continue spawning until the count is depleted

        while (count > 0)
        {
            int randomIdx = Random.Range(0, prefabsList.Length);
            Instantiate(prefabsList[randomIdx], spawnPoint.position, spawnPoint.rotation); // Spawn the prefab
            count--; // Decrement the count after each spawn
            yield return new WaitForSeconds(spawnInterval); // Wait for specified interval before spawning next prefab
        }

        //if(count <= 0)
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
