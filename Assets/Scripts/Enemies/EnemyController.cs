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

    public float chanceToChase = 0.2f;
    public bool waitForProximity = true;
    private bool playerFound = false;
    public float playerDetectionRadius = 5f;
    public float notifyRange = 3.5f;
    private float proximityCheckTimer = 0f;
    private float proximityCheckDelay = 0.5f;

    public AudioClip playerDetectedSFX;
    private AudioSource detectedSoundPlayer;
	private Animator anim;

    public bool randomiseStartPos = true;
    public float randomisePosScale = 2;

    void Awake()
    {
        detectedSoundPlayer = AddAudioSource(playerDetectedSFX, 1.0f);
    }

    public void Initialize(Vector3 position)
    {
        nav = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        player = Object.FindObjectOfType<PlayerMovement>();
		anim = GetComponent<Animator> ();
		anim.SetBool ("Idling", true);

        nav.enabled = false;
        transform.position = position;
        nav.enabled = true;

        health.ResetHealth();

        var manager = GameObject.FindObjectOfType<EnemySpawnManager>();
        manager.RegisterEnemy(this);

        //Player detection settings
        playerFound = !waitForProximity;
        checkTimer = positionCheckDelay;

        if (Random.Range(0, 10) <= (int)10*chanceToChase)
        {
            playerFound = true;
			anim.SetBool ("Idling", false);
        }

        if (randomiseStartPos)
        {
            Vector2 positionOffset = Random.insideUnitCircle;
            positionOffset *= randomisePosScale;
            Vector3 newPosition = new Vector3(transform.position.x + positionOffset.x, transform.position.y, transform.position.z + positionOffset.y);
            nav.SetDestination(newPosition);

        }
    }

    void Update()
    {
        if (player != null)
        {
            if (playerFound == false)
            {
                checkIfPlayerFound();
            }
            else
            {
                checkTimer += Time.deltaTime;
                if (checkTimer > positionCheckDelay || checkDistance > Vector3.Distance(transform.position, player.transform.position))
                {
                    checkTimer = 0f;
					anim.SetBool ("Idling", false);
                    nav.SetDestination(player.transform.position);
                }
            }
        }
    }

    private void checkIfPlayerFound()
    {
        proximityCheckTimer += Time.deltaTime;
        if (proximityCheckTimer > proximityCheckDelay)
        {
            proximityCheckTimer = 0f;
            if (playerDetectionRadius > Vector3.Distance(transform.position, player.transform.position))
            {
                notifyPlayerFound();
            }
        }
    }

    public void notifyPlayerFound()
    {
        if (playerFound == false)
        {
            detectedSoundPlayer.Play();
            playerFound = true;
            notifyEnemiesInRange();
        }
    }

    public bool hasBeenNotified()
    {
        return playerFound;
    }

    private void notifyEnemiesInRange()
    {
        //Transform parent = transform.parent;
        foreach (EnemyController enCont in GameObject.FindObjectsOfType<EnemyController>())
        {
            if (enCont.gameObject.activeSelf && !enCont.hasBeenNotified())
            {
                Transform otherTrans = enCont.GetComponent<Transform>();
                if (notifyRange > Vector3.Distance(transform.position, otherTrans.position))
                {
                    StartCoroutine(notifyOther(enCont, Random.value));
                }
            }
        }
    }

    private IEnumerator notifyOther(EnemyController otherCon, float notifyDelay)
    {
        yield return new WaitForSeconds(notifyDelay);
        otherCon.notifyPlayerFound();
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
