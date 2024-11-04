using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform parentTransform;
    public Transform spawnerLocation;
    public int enemyCount;

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (enemyCount < 5) // Loop indefinitely
        {

            // Instantiate the cube prefab at the spawner's position with no rotation
            GameObject newObject = Instantiate(cubePrefab, spawnerLocation.position, Quaternion.identity, parentTransform);
            Instantiate(newObject);

            // Wait for 10 seconds before the next spawn
            yield return new WaitForSeconds(20f);
            enemyCount++;
        }
    }
}
