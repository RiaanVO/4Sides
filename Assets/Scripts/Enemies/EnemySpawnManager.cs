using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public DropPodController DropPod;
    public int DropPodsPerWave = 5;

    private List<Vector3> spawnPoints;
    private int dropPodsRemaining = 0;
    private int enemiesActive = 0;
    private bool allEnemiesSpawned = false;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint")
            .Select(o => o.transform.position).ToList();

        if (DropPod == null)
        {
            Debug.LogError("No drop pod specified!");
        }

        StartNewWave();
    }

    private void StartNewWave()
    {
        // determine how many drop pods to spawn
        int dropPodCount = Mathf.Min(DropPodsPerWave, spawnPoints.Count);

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
        dropPodsRemaining = dropPodCount;
        foreach (var position in spawnLocations)
        {
            var dropPod = DropPod.GetPooledInstance<DropPodController>();
            dropPod.Initialize(position, OnDropPodDepleted);
        }

        enemiesActive = 0;
        allEnemiesSpawned = false;
    }

    private void OnDropPodDepleted()
    {
        dropPodsRemaining--;
        if (dropPodsRemaining <= 0)
        {
            allEnemiesSpawned = true;
        }
    }

    public void RegisterEnemy(EnemyController enemy)
    {
        enemiesActive++;

        var eventSource = enemy.GetComponent<EventSource>();
        eventSource.Subscribe("BaseHealth.Died", OnEnemyKilled);
    }

    private void OnEnemyKilled(EventSource source, string eventName)
    {
        source.Unsubscribe(eventName, OnEnemyKilled);

        enemiesActive--;
        if (allEnemiesSpawned && enemiesActive <= 0)
        {
            StartNewWave();
        }
    }
}
