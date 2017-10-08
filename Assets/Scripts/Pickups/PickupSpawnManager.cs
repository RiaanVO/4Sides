using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupSpawnManager : MonoBehaviour
{

    private List<Vector3> healthSpawnPoints;
    private HealthPickup healthPickup;
    private RapidFirePickupScript RapidFirePickup;
    private ExplosiveFirePickupScript ExplosiveFirePickup;
    private int RandomPickupToSpawn;
    private int CurrentHealthIndex;
    private int CurrentFireIndex;

    // Use this for initialization
    void Start()
    {
        healthSpawnPoints = GameObject.FindGameObjectsWithTag("HealthSpawnPoint")
            .Select(o => o.transform.position).ToList();

        healthPickup = GetComponentInChildren<HealthPickup>();
        RapidFirePickup = GetComponentInChildren<RapidFirePickupScript>();
        ExplosiveFirePickup = GetComponentInChildren<ExplosiveFirePickupScript>();
    }

    public void SpawnPickup()
    {
        if (healthSpawnPoints.Count > 1)
        {
            if (healthPickup != null)
            {
                if (healthPickup.IsCollected())
                {
                    int healthIndex = Random.Range(0, (healthSpawnPoints.Count - 1));
                    while (healthIndex == CurrentFireIndex)
                    {
                        healthIndex = Random.Range(0, (healthSpawnPoints.Count - 1));
                    }
                    healthPickup.SpawnHealthPickup(healthSpawnPoints.ElementAt(healthIndex));
                    CurrentHealthIndex = healthIndex;
                }
            }
            if (RapidFirePickup != null && ExplosiveFirePickup != null)
            {
                if (RapidFirePickup.IsCollected() || ExplosiveFirePickup.IsCollected())
                {
                    int index = Random.Range(0, healthSpawnPoints.Count);
                    while (index == CurrentHealthIndex)
                    {
                        index = Random.Range(0, healthSpawnPoints.Count);
                    }

                    RandomPickupToSpawn = Random.Range(1, 100);
                    if (RandomPickupToSpawn <= 50)
                    {
                        ExplosiveFirePickup.SpawnExplosiveFirePickup(healthSpawnPoints.ElementAt(index));
                    }
                    else if (RandomPickupToSpawn >= 51)
                    {
                        RapidFirePickup.SpawnRapidFirePickup(healthSpawnPoints.ElementAt(index));
                    }
                    CurrentFireIndex = index;
                }
            }
            else if (RapidFirePickup != null && ExplosiveFirePickup == null)
            {
                if (RapidFirePickup.IsCollected())
                {
                    int index = Random.Range(0, healthSpawnPoints.Count);
                    while (index == CurrentHealthIndex)
                    {
                        index = Random.Range(0, healthSpawnPoints.Count);
                    }
                    RapidFirePickup.SpawnRapidFirePickup(healthSpawnPoints.ElementAt(index));
                    CurrentFireIndex = index;
                }
            }
            else if (ExplosiveFirePickup != null && RapidFirePickup == null)
            {
                if (ExplosiveFirePickup.IsCollected())
                {
                    int index = Random.Range(0, healthSpawnPoints.Count);
                    while (index == CurrentHealthIndex)
                    {
                        index = Random.Range(0, healthSpawnPoints.Count);
                    }
                    ExplosiveFirePickup.SpawnExplosiveFirePickup(healthSpawnPoints.ElementAt(index));
                    CurrentFireIndex = index;
                }
            }
        }
        else
        {
            if (healthPickup != null)
            {
                if (healthPickup.IsCollected())
                    healthPickup.SpawnHealthPickup(healthSpawnPoints.ElementAt(0));
            }
            else if (RapidFirePickup != null)
            {
                if (RapidFirePickup.IsCollected())
                    RapidFirePickup.SpawnRapidFirePickup(healthSpawnPoints.ElementAt(0));
            }
            else if (ExplosiveFirePickup != null)
            {
                if (ExplosiveFirePickup.IsCollected())
                    ExplosiveFirePickup.SpawnExplosiveFirePickup(healthSpawnPoints.ElementAt(0));
            }
        }
    }

}


