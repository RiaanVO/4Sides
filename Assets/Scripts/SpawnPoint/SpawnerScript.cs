using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject enemy;
	public Material[] material;
	public Renderer rend;
	public float spawnTime;


	private float timeSinceSpawn;


	void start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
		Time.timeScale = 0;
		timeSinceSpawn = 0;

	}

	void Update () {
		timeSinceSpawn += Time.deltaTime;

		if (timeSinceSpawn >= spawnTime) {
			StartCoroutine(Spawn());
			timeSinceSpawn = 0;

		}
	}
		

	IEnumerator Spawn() {
		Instantiate (enemy, transform.position, Quaternion.identity);	
		rend.sharedMaterial = material[1];
		yield return new WaitForSeconds(1);
		rend.sharedMaterial = material[0];
	}
}
