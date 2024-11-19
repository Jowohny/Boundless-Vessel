using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : EnemyFollow
{
    void Start()
    {
        maxHealth = 60; // Crab has low health
        speed = 15f; // Crab is faster
        base.Start();
    }
}