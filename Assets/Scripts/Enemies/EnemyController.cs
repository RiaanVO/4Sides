using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyController : PooledObject
{
    private NavMeshAgent nav;
    private EnemyHealth health;
    private PlayerMovement player;

	public float checkDistance = 5f;
	public float positionCheckDelay = 0.5f;
	private float checkTimer = 0f;

	private bool playerFound = false;
	public float playerDetectionRadius = 6f;

	public AudioClip playerDetectedSFX;
	private AudioSource detectedSoundPlayer;

	void Awake(){
		detectedSoundPlayer = AddAudioSource(playerDetectedSFX, 1.0f);
	}

	public void Initialize(Vector3 position)
	{
		nav = GetComponent<NavMeshAgent>();
		health = GetComponent<EnemyHealth>();
		player = Object.FindObjectOfType<PlayerMovement>();

		nav.enabled = false;
		transform.position = position;
		nav.enabled = true;

		health.ResetHealth();

		var manager = GameObject.FindObjectOfType<EnemySpawnManager>();
		manager.RegisterEnemy(this);


		//Player detection settings
		playerFound = false;
		checkTimer = positionCheckDelay;
	}

    void Update()
    {
        if (player != null)
        {
			if (playerFound == false) {
				checkIfPlayerFound ();
			} else {
				checkTimer += Time.deltaTime;
				if (checkTimer > positionCheckDelay || checkDistance > Vector3.Distance (transform.position, player.transform.position)) {
					checkTimer = 0f;
					nav.SetDestination (player.transform.position);
				}
			}
        }
    }

	private void checkIfPlayerFound(){
		if (playerDetectionRadius > Vector3.Distance (transform.position, player.transform.position)) {
			notifyPlayerFound ();
		}
	}

	private void notifyPlayerFound(){
		if (playerFound == false) {
			detectedSoundPlayer.Play ();
			playerFound = true;
		}
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
}
