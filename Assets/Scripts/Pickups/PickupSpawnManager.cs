using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupSpawnManager : MonoBehaviour {

	private List<Vector3> healthSpawnPoints;
	private HealthPickup healthPickup;
	private RapidFirePickupScript RapidFirePickup;
	private int RandomPickupToSpawn;

	// Use this for initialization
	void Start () {
		healthSpawnPoints = GameObject.FindGameObjectsWithTag("HealthSpawnPoint")
			.Select(o => o.transform.position).ToList();

		healthPickup = GetComponentInChildren<HealthPickup> ();
		RapidFirePickup = GetComponentInChildren<RapidFirePickupScript> ();
	}

	public void SpawnPickup(){
		if (healthPickup == null || RapidFirePickup == null)
			return;
		if (healthPickup.IsCollected () || RapidFirePickup.IsCollected()) {
			int index = Random.Range(0, healthSpawnPoints.Count);

			RandomPickupToSpawn = Random.Range (1, 100);
			if (RandomPickupToSpawn <= 50) {
				healthPickup.SpawnHealthPickup (healthSpawnPoints.ElementAt (index));
			}
			else if (RandomPickupToSpawn >=51) {
				RapidFirePickup.SpawnRapidFirePickup (healthSpawnPoints.ElementAt (index));
			}
		}
	}

}


