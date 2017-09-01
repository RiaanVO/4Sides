using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public DropPodController DropPod;
    public int DropPodsPerWave = 5;

    private List<Vector3> spawnPoints;

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
        foreach (var position in spawnLocations)
        {
            var dropPod = DropPod.GetPooledInstance<DropPodController>();
            dropPod.Initialize(position);
        }
    }
}
