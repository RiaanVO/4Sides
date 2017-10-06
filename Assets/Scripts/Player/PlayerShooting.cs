using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public Bullet originBullet;
    public float FireRate = 0.001f;
    public Light MuzzleFlash;
    public Transform shootingPoint;

    private Bullet CurrentBullet;
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
        CurrentBullet = originBullet;
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
        var bullet = CurrentBullet.GetPooledInstance<Bullet>();
        bullet.Initialize(shootingPoint.position, transform.rotation);
        lastFiredTimestamp = Time.time;
    }


    public void ChangeFireRate(float duration, float targetFireRate, Bullet newBullet)
    {
		StartCoroutine (changeFireRate (duration, targetFireRate, newBullet));
        //fireRateTimeRemaining = duration;
        //currentFireRate = targetFireRate;
    }
    

	private IEnumerator changeFireRate(float duration, float targetFireRate, Bullet newBullet)
    {
		currentFireRate = targetFireRate;
        if (newBullet != null)
            CurrentBullet = newBullet;
        yield return new WaitForSeconds(duration);
		currentFireRate = FireRate;
        CurrentBullet = originBullet;

    }
}