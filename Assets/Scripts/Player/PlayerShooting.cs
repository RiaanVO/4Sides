using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Bullet Bullet;
    public float FireRate = 1.0f;

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
        }
    }

    void Shoot()
    {
        var bullet = Bullet.GetPooledInstance<Bullet>();
        bullet.Initialize(transform.position, transform.rotation);
        lastFiredTimestamp = Time.time;
    }
}