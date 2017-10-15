using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPickup : MonoBehaviour
{

    private const string PLAYER_TAG = "Player";

    public float laserFireDuration = 10f;
    public int laserFireDamage = 5;
    public float laserFireTickRate = 0.1f;

    public AudioClip laserSpawnSFX;
    public AudioClip laserCollectedSFX;
	public AudioClip UnableToPickupSFX;


    private Renderer bulletMaterial;
    private AudioSource source;
    public GameObject model;
    public GameObject pickupLight;
    private bool isCollected = true;

    private PickupAnimation pickupAnimation;

    public void Start()
    {
        source = GetComponent<AudioSource>();
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

        if (source != null && laserSpawnSFX != null)
        {
            source.clip = laserSpawnSFX;
            source.Play();
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
				if (player.isUsingPickup ()) {
					if (source != null && UnableToPickupSFX != null)
					{
						source.clip = UnableToPickupSFX;
						source.Play();
					}
					return;
				}


                isCollected = true;
                if (source != null && laserCollectedSFX != null)
                {
                    source.clip = laserCollectedSFX;
                    source.Play();
                }

                SetVisibility(false);
                player.ChangeToLaserShooting(laserFireDuration, laserFireDamage, laserFireTickRate);
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
