using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform boatRb;   // Boat's Rigidbody reference

    void Update()
    {

        // Update the agent's destination to the boat's position
        enemy.SetDestination(boatRb.position);

    }
}
