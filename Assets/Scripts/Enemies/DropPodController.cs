using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class DropPodController : PooledObject
{
    public float MinDropHeight = 15.0f;
    public float MaxDropHeight = 35.0f;
    public EnemyController Enemy;
    public float TimeBetweenSpawns = 1.0f;

    public AudioClip SpawnedSound;
    public AudioClip LandedSound;
    public AudioClip OpeningSound;

    public float MaxCameraShake = 20.0f;
    public float MinCameraShake = 3.0f;
    public float ShakeRange = 5.0f;

    private Animator animator;
    private Rigidbody body;
    private AudioSource spawnedSound;
    private AudioSource landedSound;
    private AudioSource openingSound;
    private PlayerMovement player;
    private CameraShake shaker;

    private bool hasLanded;
    private bool canSpawn;
    private float lastSpawnedTimestamp;
    private int enemiesRemaining;
    private Action onAllEnemiesSpawnedCallback;

	//Floor landing test
	private Vector3 spawnPosition;

    void Awake()
    {
		spawnedSound = AddAudioSource (SpawnedSound, 1f);//0.75f);
        landedSound = AddAudioSource(LandedSound, 1.0f);
		openingSound = AddAudioSource (OpeningSound, 1f);//0.75f);

        player = GameObject.FindObjectOfType<PlayerMovement>();
        shaker = Camera.main.GetComponent<CameraShake>();
    }

    private AudioSource AddAudioSource(AudioClip clip, float volume)
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.playOnAwake = false;
        source.spatialBlend = 0.75f;
        return source;
    }

    public void Initialize(Vector3 position, int enemiesToSpawn, Action onAllEnemiesSpawnedCallback)
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

        float dropHeight = Random.Range(MinDropHeight, MaxDropHeight);
        transform.position = position + (Vector3.up * dropHeight);
		spawnPosition = position;

        hasLanded = false;
        body.isKinematic = false;
        animator.SetBool("HasLanded", false);

        canSpawn = false;
        lastSpawnedTimestamp = Time.time;
        enemiesRemaining = enemiesToSpawn;

        this.onAllEnemiesSpawnedCallback = onAllEnemiesSpawnedCallback;

        spawnedSound.pitch = Random.Range(0.97f, 1.03f);
        spawnedSound.PlayDelayed(dropHeight * 0.02f);
    }

    void Update()
    {
		if (!hasLanded)
		{
			if (transform.position.y < spawnPosition.y) {
				transform.position = spawnPosition;
				OnLanding ();
			}
		}

        if (canSpawn && Time.time - lastSpawnedTimestamp > TimeBetweenSpawns)
        {
            // spawn enemy
            SpawnEnemy();
            lastSpawnedTimestamp = Time.time;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasLanded)
        {
            return;
        }

		//OnLanding ();
    }

	private void OnLanding(){
		hasLanded = true;
		body.isKinematic = true;
		animator.SetBool("HasLanded", true);

		// play landed and opening sound
		landedSound.pitch = Random.Range(0.85f, 1.15f);
		landedSound.Play();
		openingSound.pitch = Random.Range(0.85f, 1.15f);
		openingSound.PlayDelayed(0.5f);

		// shake camera
		if (shaker != null && player != null)
		{
			float distance = Vector2.Distance(player.transform.position, transform.position);
			float amount = (ShakeRange - distance) * MaxCameraShake;
			shaker.RandomShake(Mathf.Clamp(amount, MinCameraShake, MaxCameraShake));
		}
	}

    public void StartSpawning()
    {
        if (enemiesRemaining > 0)
        {
            canSpawn = true;
        }
        else
        {
            animator.SetTrigger("OnAllEnemiesSpawned");
            onAllEnemiesSpawnedCallback();
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Enemy.GetPooledInstance<EnemyController>();
        enemy.Initialize(transform.position);
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            canSpawn = false;
            animator.SetTrigger("OnAllEnemiesSpawned");
            onAllEnemiesSpawnedCallback();
        }
    }
}
