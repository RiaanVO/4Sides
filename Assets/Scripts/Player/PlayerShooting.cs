using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Bullet Bullet;
    public float FireRate = 1.0f;
    public Light MuzzleFlash;

    private float lastFiredTimestamp;

    void Start()
    {
        lastFiredTimestamp = Time.time;
    }

    void Update()
    {
        if (Input.GetButton("Fire") && Time.time - lastFiredTimestamp >= FireRate)
        {
            Shoot();
            if (MuzzleFlash != null)
            {
                MuzzleFlash.enabled = true;
            }
        }
        else
        {
            if (MuzzleFlash != null)
            {
                MuzzleFlash.enabled = false;
            }
        }
    }

    void Shoot()
    {
        var bullet = Bullet.GetPooledInstance<Bullet>();
        bullet.Initialize(transform.position, transform.rotation);
        lastFiredTimestamp = Time.time;
    }
}