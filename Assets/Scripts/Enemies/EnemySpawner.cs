using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController Enemy;
    public Vector3 EnemySpawnOffset;
    public float TimeBetweenSpawns = 1.0f;
    public int EnemiesToSpawn = 5;

    private float lastSpawnedTimestamp;
    private int enemiesRemaining;

    public void Initialize()
    {
        lastSpawnedTimestamp = Time.time;
        enemiesRemaining = EnemiesToSpawn;
    }

    void Update()
    {
        if (enemiesRemaining > 0 && Time.time - lastSpawnedTimestamp > TimeBetweenSpawns)
        {
            // spawn enemy
            SpawnEnemy();
            lastSpawnedTimestamp = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Enemy.GetPooledInstance<EnemyController>();
        enemy.Initialize(transform.position + EnemySpawnOffset);
        enemiesRemaining--;
    }
}
