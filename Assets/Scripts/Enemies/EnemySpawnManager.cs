using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float InitialTimeBetweenSpawns = 5.0f;
    public float MinimumTimeBetweenSpawns = 0.1f;
    public float TimeBetweenSpawnRateIncreases = 10.0f;
    public float SpawnIntervalDecrease = 0.5f;

    private EnemySpawner[] spawners;
    private float currentTimeBetweenSpawns;

    private float lastSpawnedTimestamp;
    private float rateLastIncreasedTimestamp;

    void Start()
    {
        spawners = Object.FindObjectsOfType<EnemySpawner>();
        currentTimeBetweenSpawns = InitialTimeBetweenSpawns;

        lastSpawnedTimestamp = Time.time;
        rateLastIncreasedTimestamp = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnedTimestamp > currentTimeBetweenSpawns)
        {
            // spawn an enemy
            Spawn();
            lastSpawnedTimestamp = Time.time;
        }

        if (Time.time - rateLastIncreasedTimestamp > TimeBetweenSpawnRateIncreases)
        {
            // increase spawn rate
            currentTimeBetweenSpawns = Mathf.Max(currentTimeBetweenSpawns - SpawnIntervalDecrease,
                MinimumTimeBetweenSpawns);
            rateLastIncreasedTimestamp = Time.time;
        }
    }

    private void Spawn()
    {
        if (spawners.Length == 0) return;

        // pick a random spawner and tell it to spawn an enemy
        var spawner = spawners[Random.Range(0, spawners.Length)];
        spawner.Spawn();
    }
}
