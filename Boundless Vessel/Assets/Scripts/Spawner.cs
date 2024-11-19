using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform parentTransform;
    public Transform spawnerLocation;
    public int maxEnemiesToSpawn = 5; // Max enemies to spawn in this section
    public float spawnDelay = 30f;    // Delay between each spawn

    private int enemyCount = 0;       // Keep track of how many enemies have spawned

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (enemyCount < maxEnemiesToSpawn)
        {
            // Instantiate the enemy prefab at the spawner's position with no rotation
            GameObject newEnemy = Instantiate(enemyPrefab, spawnerLocation.position, Quaternion.identity, parentTransform);

            enemyCount++; // Increment enemy count

            // Wait for the next spawn delay
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void DestroyAllEnemies()
    {
        // Destroy all spawned enemies under this spawner
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
        enemyCount = 0; // Reset enemy count when enemies are destroyed
    }
}
