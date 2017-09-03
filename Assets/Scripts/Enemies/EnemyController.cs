using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyController : PooledObject
{
    private NavMeshAgent nav;
    private EnemyHealth health;
    private PlayerMovement player;

    void Update()
    {
        if (player != null)
        {
            nav.SetDestination(player.transform.position);
        }
    }

    public void Initialize(Vector3 position)
    {
        nav = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        player = Object.FindObjectOfType<PlayerMovement>();

        nav.enabled = false;
        transform.position = position;
        nav.enabled = true;

        health.ResetHealth();

        var manager = GameObject.FindObjectOfType<EnemySpawnManager>();
        manager.RegisterEnemy(this);
    }
}
