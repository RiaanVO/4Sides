using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float InitialTimeBetweenSpawns = 5.0f;
    public float MinimumTimeBetweenSpawns = 0.1f;
    public float TimeBetweenSpawnRateIncreases = 10.0f;
    public float SpawnIntervalDecrease = 0.5f;
    public DropPodController DropPod;

    private List<Vector3> spawnPoints;
    private float currentTimeBetweenSpawns;

    private float lastSpawnedTimestamp;
    private float rateLastIncreasedTimestamp;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint")
            .Select(o => o.transform.position).ToList();
        currentTimeBetweenSpawns = InitialTimeBetweenSpawns;

        lastSpawnedTimestamp = Time.time;
        rateLastIncreasedTimestamp = Time.time;

        if (DropPod == null)
        {
            Debug.LogError("No drop pod specified!");
        }

        StartNewWave();
    }

    void Update()
    {
        /*
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
        */
    }

    private void StartNewWave()
    {
        // determine how many drop pods to spawn
        int dropPodCount = Mathf.Min(5, spawnPoints.Count);

        List<int> possibleSpawnIndices = Enumerable.Range(0, spawnPoints.Count).ToList();
        List<Vector3> spawnLocations = new List<Vector3>();
        for (int i = 0; i < dropPodCount; i++)
        {
            // pick a random spawn point
            int index = Random.Range(0, possibleSpawnIndices.Count);
            spawnLocations.Add(spawnPoints[possibleSpawnIndices[index]]);
            possibleSpawnIndices.RemoveAt(index);
        }

        // create drop pods
        foreach (var position in spawnLocations)
        {
            var dropPod = DropPod.GetPooledInstance<DropPodController>();
            dropPod.Initialize(position);
        }
    }

    private void Spawn()
    {
        /*
        if (spawnPoints.Count == 0) return;

        // pick a random spawner and tell it to spawn an enemy
        var spawner = spawners[Random.Range(0, spawners.Length)];
        spawner.Spawn();
        */
    }
}
