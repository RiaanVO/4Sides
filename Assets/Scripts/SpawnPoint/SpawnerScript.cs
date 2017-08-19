using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject enemy;
	public float spawnTime;
	private float timeSinceSpawn;




	void start () {
		Time.timeScale = 0;
		timeSinceSpawn = 0;
		SpawnEnemy();

	}

	void Update () {
		timeSinceSpawn += Time.deltaTime;

		if (timeSinceSpawn >= spawnTime) {
			SpawnEnemy ();
			timeSinceSpawn = 0;
		}
	}

	void SpawnEnemy() 
	{
		Instantiate (enemy, transform.position, Quaternion.identity);	
	}
}
