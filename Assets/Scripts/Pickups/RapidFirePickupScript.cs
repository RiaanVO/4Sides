using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickupScript : MonoBehaviour {

	private const string PLAYER_TAG = "Player";
	public float RapidFireRate = 0.001f;
	public int RapidFireRateDuration = 10;

	public AudioClip RapidFireSpawnSFX;
	public AudioClip RapidFireCollectedSFX;

	private AudioSource audioSource;
	public GameObject model;
	public GameObject pickupLight;
	private bool isCollected = true;

	private PickupAnimation pickupAnimation;

	public void Start(){
		audioSource = GetComponent<AudioSource> ();
		SetVisability (false);
	}




	public void SpawnRapidFirePickup(Vector3 newPosition){
		if (pickupAnimation == null) {
			pickupAnimation = GetComponent<PickupAnimation> ();
		}

		pickupAnimation.SetBouncePositions (newPosition);
		transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

		if (audioSource != null && RapidFireSpawnSFX != null) {
			audioSource.PlayOneShot (RapidFireSpawnSFX);
		}
		SetVisability(true);
		isCollected = false;
	}

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag (PLAYER_TAG) && !isCollected) {
			PlayerShooting playerShooting = other.gameObject.GetComponentInParent<PlayerShooting>();
			if (playerShooting != null) {
				playerShooting.FireRate = RapidFireRate;
				isCollected = true;

				if (audioSource != null && RapidFireCollectedSFX != null) {
					audioSource.PlayOneShot (RapidFireCollectedSFX);
				}

				SetVisability (false);

				StartCoroutine (RapidFireDurationDelay());
			}
		}
	}

	private void SetVisability(bool active){
		model.SetActive (active);
		pickupLight.SetActive (active);
	}

	public bool IsCollected(){
		return isCollected;
	}


	public IEnumerator RapidFireDurationDelay() {
		PlayerShooting playerShooting = GameObject.FindGameObjectWithTag (PLAYER_TAG).GetComponentInParent<PlayerShooting> ();

	yield return new WaitForSeconds (RapidFireRateDuration);
		playerShooting.FireRate = 0.07f;
	}

}
