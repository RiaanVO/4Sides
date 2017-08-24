using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
	public Transform[] spawnPoint;
	public Renderer[] spawnPointRenderer;
    public Material IdleMaterial;
    public Material ActiveMaterial;
    public float TimeBetweenSpawns;
    public float TelegraphDuration;
	public float TimeBeforeSpawnDecrease;
	public float spawnDecreaseRateSeconds;

    private Renderer render;
    private float spawnTime;
	private float gameTime;
	private float randomChosenSpawnPoint;

    void Start()
    {
		gameTime = 0;
		spawnPointRenderer = GetComponentsInChildren<Renderer> ();
		foreach (Renderer render in spawnPointRenderer) {
			render.enabled = true;
			render.material = IdleMaterial;
		}
        spawnTime = Time.time;
    }

    void Update()
    {
		gameTime += Time.deltaTime;
		Debug.Log (gameTime);
		if (gameTime >= TimeBeforeSpawnDecrease) {
			TimeBetweenSpawns += spawnDecreaseRateSeconds;
			gameTime = 0;
		}


        if (Time.time - spawnTime > TimeBetweenSpawns - TelegraphDuration)
        {
            StartCoroutine(Spawn());
            spawnTime = Time.time;
        }
    }

    IEnumerator Spawn()
    {
    
		randomChosenSpawnPoint = Random.Range (0, spawnPoint.Length);
		if (randomChosenSpawnPoint == 0) 
		{
			Instantiate (Enemy, spawnPoint[0].transform.position, Quaternion.identity);
			spawnPointRenderer [1].sharedMaterial = ActiveMaterial;
			yield return new WaitForSeconds(TelegraphDuration);
			spawnPointRenderer [1].sharedMaterial = IdleMaterial;


		} 

		else if (randomChosenSpawnPoint == 1) 
		{
			Instantiate (Enemy, spawnPoint[1].transform.position, Quaternion.identity);
			spawnPointRenderer [2].sharedMaterial = ActiveMaterial;
			yield return new WaitForSeconds(TelegraphDuration);
			spawnPointRenderer [2].sharedMaterial = IdleMaterial;

		}

    }
}
