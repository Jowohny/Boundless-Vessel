using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyFollow
{
    void Start()
    {
        maxHealth = 120; // Crab has low health
        speed = 7f; // Crab is faster
        base.Start();
    }
}