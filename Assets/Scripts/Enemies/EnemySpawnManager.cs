using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public static readonly string CHANNEL_WAVE = "EnemySpawnManager.Wave";
    public static readonly string CHANNEL_DISPLAY_WAVE = "EnemySpawnManager.DisplayWave";

    [Serializable]
    public class Wave
    {
        public int DropPodCount = 5;
    }

    public DropPodController DropPod;
    public List<Wave> Waves;

    private DataProvider data;
    private List<Vector3> spawnPoints;
    private int dropPodsRemaining = 0;
    private int enemiesActive = 0;
    private bool allEnemiesSpawned = false;
    private int wave = 0;

    void Start()
    {
        data = GetComponent<DataProvider>();
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint")
            .Select(o => o.transform.position).ToList();

        if (DropPod == null)
        {
            Debug.LogError("No drop pod specified!");
        }
        if (Waves == null || Waves.Count < 1)
        {
            Debug.LogError("No waves defined!");
        }

        StartNewWave();
    }

    private void StartNewWave()
    {
        if (wave >= Waves.Count)
        {
            return;
        }

        // determine how many drop pods to spawn
        var currentWave = Waves[wave];
        int dropPodCount = Mathf.Min(currentWave.DropPodCount, spawnPoints.Count);

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

        // increment wave
        wave++;
        if (data != null)
        {
            data.UpdateChannel(CHANNEL_WAVE, wave);
            data.UpdateChannel(CHANNEL_DISPLAY_WAVE, wave.ToString());
        }
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
