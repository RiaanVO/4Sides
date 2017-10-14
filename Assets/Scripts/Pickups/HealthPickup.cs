using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    private const string PLAYER_TAG = "Player";
    public int healthAmount = 50;

    public AudioClip healthSpawnSFX;
    public AudioClip healthCollectedSFX;

    private AudioSource source;
    public GameObject model;
    public GameObject pickupLight;
    private bool isCollected = true;

    private PickupAnimation pickupAnimation;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        SetVisability(false);
    }

    public void SpawnHealthPickup(Vector3 newPosition)
    {
        if (pickupAnimation == null)
        {
            pickupAnimation = GetComponent<PickupAnimation>();
        }

        pickupAnimation.SetBouncePositions(newPosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

        if (source != null && healthSpawnSFX != null)
        {
            source.clip = healthSpawnSFX;
            source.Play();
        }
        SetVisability(true);
        isCollected = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG) && !isCollected)
        {
            BaseHealth playerHealth = other.gameObject.GetComponentInParent<BaseHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                isCollected = true;

                if (source != null && healthCollectedSFX != null)
                {
                    source.clip = healthCollectedSFX;
                    source.Play();
                }

                SetVisability(false);
            }
        }
    }

    private void SetVisability(bool active)
    {
        model.SetActive(active);
        pickupLight.SetActive(active);
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
