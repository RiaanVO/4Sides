using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDodger : MonoBehaviour {

	public Rigidbody parentRigidBody;
	public float TeleportDistance = 5f;
	public float TeleportCooldown = 3f;
	private Vector3 TeleportToPosition;
	private int RandomDirection;
	private ParticleSystem TeleportEffect;
	private float TimeSinceLastHit;


	private float lerpTime = 1;

	void Start () {
		parentRigidBody = transform.parent.GetComponent<Rigidbody> ();
		TeleportEffect = transform.parent.GetComponent<ParticleSystem> ();
		TimeSinceLastHit = 0;
	}
		

	void Update () {
		TimeSinceLastHit -= Time.deltaTime;
	}


	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject.CompareTag("Bullet") && (TimeSinceLastHit <= 0)) 
		{
			StartCoroutine (PlayTeleportEffect ());
			TimeSinceLastHit = TeleportCooldown;
			RandomDirection = Random.Range (1, 4);
			if (RandomDirection == 1) {
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
				
		}

	}


	private IEnumerator PlayTeleportEffect(){
		TeleportEffect.Play ();
		yield return new WaitForSeconds (0.5f);
		TeleportEffect.Stop ();
	}
}
