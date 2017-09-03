using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthPickupSpawnManager : MonoBehaviour {

	private List<Vector3> healthSpawnPoints;
	private HealthPickup healthPickup;

	// Use this for initialization
	void Start () {
		healthSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint")
			.Select(o => o.transform.position).ToList();

		healthPickup = GetComponentInChildren<HealthPickup> ();
	}

	public void SpawnHealthPickup(){
		if (healthPickup == null)
			return;
		if (healthPickup.IsCollected ()) {
			int index = Random.Range(0, healthSpawnPoints.Count);
			healthPickup.SpawnHealthPickup (healthSpawnPoints.ElementAt (index));
		}
	}

}
