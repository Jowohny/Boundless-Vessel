using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject cubePrefab;

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (true) // Loop indefinitely
        {
            // Instantiate the cube prefab at the spawner's position with no rotation
            Instantiate(cubePrefab, transform.position, Quaternion.identity);

            // Wait for 10 seconds before the next spawn
            yield return new WaitForSeconds(10f);
        }
    }
}
