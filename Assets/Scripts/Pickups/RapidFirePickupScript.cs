using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickupScript : MonoBehaviour
{

    private const string PLAYER_TAG = "Player";
    public float RapidFireRate = 0.001f;
    public int RapidFireRateDuration = 10;

    public AudioClip RapidFireSpawnSFX;
    public AudioClip RapidFireCollectedSFX;

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

    public void SpawnRapidFirePickup(Vector3 newPosition)
    {
        if (pickupAnimation == null)
        {
            pickupAnimation = GetComponent<PickupAnimation>();
        }

        pickupAnimation.SetBouncePositions(newPosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

        if (audioSource != null && RapidFireSpawnSFX != null)
        {
            audioSource.PlayOneShot(RapidFireSpawnSFX);
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
                if (audioSource != null && RapidFireCollectedSFX != null)
                {
                    audioSource.PlayOneShot(RapidFireCollectedSFX);
                }

                SetVisibility(false);
                player.IncreaseFireRate(RapidFireRateDuration, RapidFireRate);
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
