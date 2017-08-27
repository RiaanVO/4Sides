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

    private Renderer render;
    private bool isSpawning;
    private float telegraphStartTimestamp;

    void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;
        render.material = IdleMaterial;

        isSpawning = false;
        telegraphStartTimestamp = Time.time;
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
    }

    private void SpawnEnemy()
    {
        var enemy = Enemy.GetPooledInstance<EnemyController>();
        enemy.Initialize(transform.position + EnemySpawnOffset);
    }

    public void Spawn()
    {
        // if we were mid-spawn, spawn the previous enemy early
        if (isSpawning)
        {
            SpawnEnemy();
        }

        isSpawning = true;
        telegraphStartTimestamp = Time.time;
    }
}
