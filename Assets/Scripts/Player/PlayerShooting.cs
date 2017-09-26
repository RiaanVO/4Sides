using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public Bullet Bullet;
    public float FireRate = 0.001f;
    public Light MuzzleFlash;
    public Transform shootingPoint;

    private float lastFiredTimestamp;
    private AudioSource fireSound;

    //private float fireRateTimeRemaining;
    private float currentFireRate;

    public bool IsShooting { get; private set; }

    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        lastFiredTimestamp = Time.time;
        currentFireRate = FireRate;
    }

    void Update()
    {
		/*
        if (fireRateTimeRemaining > 0)
        {
            fireRateTimeRemaining -= Time.deltaTime;
            if (fireRateTimeRemaining < 0)
            {
                currentFireRate = FireRate;
            }
        }
        */

        IsShooting = Input.GetButton("Fire");
        if (IsShooting && Time.time - lastFiredTimestamp >= currentFireRate)
        {
            Shoot();
            if (MuzzleFlash != null)
            {
                fireSound.pitch = Random.Range(0.9f, 1.1f);
                fireSound.Play();
                MuzzleFlash.enabled = true;
            }
        }
        else if (MuzzleFlash != null)
        {
            MuzzleFlash.enabled = false;
        }
    }

    void Shoot()
    {
        var bullet = Bullet.GetPooledInstance<Bullet>();
        bullet.Initialize(shootingPoint.position, transform.rotation);
        lastFiredTimestamp = Time.time;
    }


    public void IncreaseFireRate(float duration, float targetFireRate)
    {
		StartCoroutine (increaseFireRate (duration, targetFireRate));
        //fireRateTimeRemaining = duration;
        //currentFireRate = targetFireRate;
    }
    

	private IEnumerator increaseFireRate(float duration, float targetFireRate){
		currentFireRate = targetFireRate;
		yield return new WaitForSeconds(duration);
		currentFireRate = FireRate; 
	}
}