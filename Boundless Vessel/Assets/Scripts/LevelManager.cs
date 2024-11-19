using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] level1Spawners; // Spawners for level 1
    public GameObject[] level2Spawners; // Spawners for level 2
    public GameObject[] level3Spawners; // Spawners for level 3

    public GameObject[] level1Enemies; // Enemies for level 1
    public GameObject[] level2Enemies; // Enemies for level 2
    public GameObject[] level3Enemies; // Enemies for level 3


    void Start()
    {
        DeactivateLevel2(); // Deactivate enemies and spawners from level 1
        DeactivateLevel3(); // Deactivate enemies and spawners from level 1
        ActivateSpawners(level1Spawners); // Activate spawners for level 2
        SpawnEnemies(level1Enemies); // Spawn level 2 enemies
    }

    // Call when transitioning to level 2
    public void ActivateLevel2()
    {
        DeactivateLevel1(); // Deactivate enemies and spawners from level 1
        DeactivateLevel3(); // Deactivate enemies and spawners from level 1
        ActivateSpawners(level2Spawners); // Activate spawners for level 2
        SpawnEnemies(level2Enemies); // Spawn level 2 enemies
    }

    // Call when transitioning to level 3
    public void ActivateLevel3()
    {
        DeactivateLevel1(); // Deactivate enemies and spawners from level 2
        DeactivateLevel2(); // Deactivate enemies and spawners from level 2
        ActivateSpawners(level3Spawners); // Activate spawners for level 3
        SpawnEnemies(level3Enemies); // Spawn level 3 enemies
    }

    private void DeactivateLevel1()
    {
        DeactivateSpawners(level1Spawners);
        DestroyEnemies(level1Enemies);
    }

    private void DeactivateLevel2()
    {
        DeactivateSpawners(level2Spawners);
        DestroyEnemies(level2Enemies);
    }

    private void DeactivateLevel3()
    {
        DeactivateSpawners(level3Spawners);
        DestroyEnemies(level3Enemies);
    }

    private void DeactivateSpawners(GameObject[] spawners)
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false); // Deactivate each spawner
        }
    }

    private void DestroyEnemies(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // Destroy each enemy
        }
    }

    private void ActivateSpawners(GameObject[] spawners)
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(true); // Activate the spawners
        }
    }

    private void SpawnEnemies(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            // Instantiate the enemy at its spawn location, if needed
            Instantiate(enemy, enemy.transform.position, Quaternion.identity);
        }
    }
}
