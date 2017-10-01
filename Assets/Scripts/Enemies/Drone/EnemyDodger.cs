using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDodger : MonoBehaviour {

	private AudioSource TeleportSounds;
	public float TeleportDistance = 5f;
	public float TeleportCooldown = 3f;
	public float InvisibleTime = 0.8f;
	public float MaterializationTIme = 0.3f;

	private float lerpTime = 1;
	private Rigidbody parentRigidBody;
	private Vector3 TeleportToPosition;
	private int RandomDirection;
	private ParticleSystem TeleportEffect;
	private Collider parentCollider;
	private Collider barrierCollider;
	private MeshRenderer[] AllDroneRenderer;
	private float TimeSinceLastHit;
	private NavMeshAgent dodgerNavMeshAgent;
	private bool isTeleporting;
	private bool isSpawning;




	void Start () {
		isSpawning = true;
		dodgerNavMeshAgent = transform.parent.GetComponent<NavMeshAgent> ();
		parentCollider = transform.parent.GetComponent<Collider> ();
		barrierCollider = transform.GetComponent<Collider> ();
		AllDroneRenderer = transform.parent.GetComponentsInChildren<MeshRenderer> ();
		TeleportSounds = GetComponent<AudioSource> ();

		foreach (Renderer rend in AllDroneRenderer) {
			rend.enabled = true;
		}

		parentRigidBody = transform.parent.GetComponent<Rigidbody> ();
		TeleportEffect = transform.parent.GetComponent<ParticleSystem> ();
		TimeSinceLastHit = 0;
		StartCoroutine (DuringSpawnBarrierDelay());
	}
		

	void Update () {
		TimeSinceLastHit -= Time.deltaTime;
	}


	void OnTriggerEnter (Collider other) 
	{

		if (other.gameObject.CompareTag ("Bullet") && (TimeSinceLastHit <= 0) && isSpawning == false) {
			TeleportSounds.Play ();
			StartCoroutine (PlayTeleportEffect ());
			TimeSinceLastHit = TeleportCooldown;
		}

	}



	private IEnumerator PlayTeleportEffect(){
		TeleportEffect.Play ();
		parentCollider.enabled = false;
		barrierCollider.enabled = false;
	
		foreach (Renderer rend in AllDroneRenderer) {
			rend.enabled = false;
		}

		dodgerNavMeshAgent.speed = 0f;
		RandomDirection = Random.Range (1, 5);
		if (RandomDirection == 1 || RandomDirection == 5 ) {
			TeleportToPosition = transform.position + Vector3.forward * TeleportDistance;
			parentRigidBody.transform.position = Vector3.Lerp (transform.position, TeleportToPosition, lerpTime);
		}
		else if (RandomDirection == 2) {
			TeleportToPosition = transform.position + Vector3.back * TeleportDistance;
			parentRigidBody.transform.position = Vector3.Lerp (transform.position, TeleportToPosition, lerpTime);
		}
		else if (RandomDirection == 3) {
			TeleportToPosition = transform.position + Vector3.left * TeleportDistance;
			parentRigidBody.transform.position = Vector3.Lerp (transform.position, TeleportToPosition, lerpTime);
		}
		else if (RandomDirection == 4) {
			TeleportToPosition = transform.position + Vector3.right * TeleportDistance;
			parentRigidBody.transform.position = Vector3.Lerp (transform.position, TeleportToPosition, lerpTime);
		}
			
		yield return new WaitForSeconds (InvisibleTime);

		foreach (Renderer rend in AllDroneRenderer) {
			rend.enabled = true;
		}
		TeleportSounds.Stop ();
		yield return new WaitForSeconds (MaterializationTIme);
		dodgerNavMeshAgent.speed = 2f;


		parentCollider.enabled = true;
		barrierCollider.enabled = true;

		TeleportEffect.Stop ();
	}


	private IEnumerator DuringSpawnBarrierDelay() {
		yield return new WaitForSeconds (2);
		isSpawning = false;
	}

}
