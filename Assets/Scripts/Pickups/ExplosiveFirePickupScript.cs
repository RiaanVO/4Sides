using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveFirePickupScript : MonoBehaviour
{

    private const string PLAYER_TAG = "Player";
    public float ExplosiveFireRate = 0.2f;
    public int ExplosiveFireRateDuration = 10;

    public Bullet NewBullet;

    public AudioClip ExplosiveFireSpawnSFX;
    public AudioClip ExplosiveFireCollectedSFX;

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

    public void SpawnExplosiveFirePickup(Vector3 newPosition)
    {
        if (pickupAnimation == null)
        {
            pickupAnimation = GetComponent<PickupAnimation>();
        }

        pickupAnimation.SetBouncePositions(newPosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

        if (source != null && ExplosiveFireSpawnSFX != null)
        {
            source.clip = ExplosiveFireSpawnSFX;
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
                if (player.isUsingPickup()) return;

                isCollected = true;
                if (source != null && ExplosiveFireCollectedSFX != null)
                {
                    source.clip = ExplosiveFireCollectedSFX;
                    source.Play();
                }

                SetVisibility(false);
                player.ChangeFireRate(ExplosiveFireRateDuration, ExplosiveFireRate, NewBullet);
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
