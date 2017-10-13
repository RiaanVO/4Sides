using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPickup : MonoBehaviour {

	private const string PLAYER_TAG = "Player";

	public float laserFireDuration = 10f;
	public float laserFireDamage = 1f;
	public float laserFireTickRate = 0.1f;

	public AudioClip laserSpawnSFX;
	public AudioClip laserCollectedSFX;

	private Renderer bulletMaterial;
	private AudioSource audioSource;
	public GameObject model;
	public GameObject pickupLight;
	private bool isCollected = true;

	private PickupAnimation pickupAnimation;

	public void Start()
	{
			audioSource = GetComponent<AudioSource>();
			SetVisibility(false);
	}

	public void SpawnLaserPickup(Vector3 newPosition)
	{
			if (pickupAnimation == null)
			{
					pickupAnimation = GetComponent<PickupAnimation>();
			}

			pickupAnimation.SetBouncePositions(newPosition);
			transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

			if (audioSource != null && laserSpawnSFX != null)
			{
					audioSource.PlayOneShot(laserSpawnSFX);
			}

			SetVisibility(true);
			isCollected = false;
	}

	public void OnTriggerEnter(Collider other)
	{
			if (other.gameObject.CompareTag(PLAYER_TAG) && !isCollected)
			{
					PlayerShooting player = other.gameObject.GetComponentInParent<PlayerShooting>();
					if (player != null)
					{
							isCollected = true;
							if (audioSource != null && laserCollectedSFX != null)
							{
									audioSource.PlayOneShot(laserCollectedSFX);
							}

							SetVisibility(false);
							//player.ChangeFireRate(RapidFireRateDuration, RapidFireRate, NewBullet);
					}
			}
	}

	private void SetVisibility(bool active)
	{
			model.SetActive(active);
			pickupLight.SetActive(active);
	}

	public bool IsCollected()
	{
			return isCollected;
	}
}
