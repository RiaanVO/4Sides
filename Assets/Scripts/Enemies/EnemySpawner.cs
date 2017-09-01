using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EnemySpawner : MonoBehaviour
{
    public EnemyController Enemy;
    public Material IdleMaterial;
    public Material ActiveMaterial;
    public float TelegraphDuration = 1.0f;
    public Vector3 EnemySpawnOffset;
    public float TimeBetweenSpawns = 1.0f;

    private Renderer render;
    private bool isSpawning;
    private float telegraphStartTimestamp;
    private float lastSpawnedTimestamp;

    public void Initialize()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;
        render.material = IdleMaterial;

        isSpawning = false;
        telegraphStartTimestamp = Time.time;
        lastSpawnedTimestamp = Time.time;
    }

    void Update()
    {
        // are we telegraphing a spawn?
        if (isSpawning)
        {
            if (Time.time - telegraphStartTimestamp > TelegraphDuration)
            {
                // spawn the enemy
                SpawnEnemy();
                isSpawning = false;
                render.material = IdleMaterial;
            }
            else
            {
                // blink to telegraph
                render.material = Time.time % 0.3f > 0.15f ? IdleMaterial : ActiveMaterial;
            }
        }
        else
        {
            if (Time.time - lastSpawnedTimestamp > TimeBetweenSpawns)
            {
                // start telegraphing
                StartSpawning();
                lastSpawnedTimestamp = Time.time;
            }
        }
    }

    private void StartSpawning()
    {
        // if we were mid-spawn, spawn the previous enemy early
        if (isSpawning)
        {
            SpawnEnemy();
        }

        isSpawning = true;
        telegraphStartTimestamp = Time.time;
    }

    private void SpawnEnemy()
    {
        var enemy = Enemy.GetPooledInstance<EnemyController>();
        enemy.Initialize(transform.position + EnemySpawnOffset);
    }
}
