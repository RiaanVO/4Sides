using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	private const string PLAYER_TAG = "Player";
	public int healthAmount = 50;

	public AudioClip healthSpawnSFX;
	public AudioClip healthCollectedSFX;

	private AudioSource audioSource;
	public GameObject model;
	public GameObject pickupLight;
	private bool wasCollected = false;

	public void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	public void SpawnHealthPickup(Vector3 position){
		transform.position = position;
		audioSource.PlayOneShot (healthSpawnSFX);
		SetVisability(true);
		wasCollected = false;
	}

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag (PLAYER_TAG) && !wasCollected) {
			BaseHealth playerHealth = other.gameObject.GetComponentInParent<BaseHealth> ();

			if (playerHealth != null) {
				playerHealth.Heal (healthAmount);
				wasCollected = true;

				if (audioSource != null && healthCollectedSFX != null) {
					audioSource.PlayOneShot (healthCollectedSFX);
				}

				SetVisability (false);
			}
		}
	}

	private void SetVisability(bool active){
		model.SetActive (active);
		pickupLight.SetActive (active);
	}
}
