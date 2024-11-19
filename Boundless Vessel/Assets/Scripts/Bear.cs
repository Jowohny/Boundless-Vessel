using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : EnemyFollow
{
    void Start()
    {
        maxHealth = 100; // Crab has low health
        speed = 18f; // Crab is faster
        base.Start();
    }
}