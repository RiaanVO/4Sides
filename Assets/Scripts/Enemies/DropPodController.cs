using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class DropPodController : PooledObject
{
    public float MinDropHeight = 15.0f;
    public float MaxDropHeight = 35.0f;
    public EnemyController Enemy;
    public float TimeBetweenSpawns = 1.0f;
    public int EnemiesToSpawn = 5;

    private Animator animator;
    private Rigidbody body;

    private bool hasLanded;
    private bool canSpawn;
    private float lastSpawnedTimestamp;
    private int enemiesRemaining;
    private Action onAllEnemiesSpawnedCallback;

    public void Initialize(Vector3 position, Action onAllEnemiesSpawnedCallback)
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

        float dropHeight = Random.Range(MinDropHeight, MaxDropHeight);
        transform.position = position + (Vector3.up * dropHeight);

        hasLanded = false;
        body.isKinematic = false;
        animator.SetBool("HasLanded", false);

        canSpawn = false;
        lastSpawnedTimestamp = Time.time;
        enemiesRemaining = EnemiesToSpawn;

        this.onAllEnemiesSpawnedCallback = onAllEnemiesSpawnedCallback;
    }

    void Update()
    {
        if (canSpawn && Time.time - lastSpawnedTimestamp > TimeBetweenSpawns)
        {
            // spawn enemy
            SpawnEnemy();
            lastSpawnedTimestamp = Time.time;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasLanded)
        {
            return;
        }

        hasLanded = true;
        body.isKinematic = true;
        animator.SetBool("HasLanded", true);
    }

    public void StartSpawning()
    {
        canSpawn = true;
    }

    private void SpawnEnemy()
    {
        var enemy = Enemy.GetPooledInstance<EnemyController>();
        enemy.Initialize(transform.position);
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            canSpawn = false;
            animator.SetTrigger("OnAllEnemiesSpawned");
            onAllEnemiesSpawnedCallback();
        }
    }
}
