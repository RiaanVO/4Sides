using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Bullet Bullet;
    public float FireRate = 0.001f;
    public Light MuzzleFlash;

    private float lastFiredTimestamp;
    private AudioSource fireSound;

	public Transform shootingPoint;

    public bool IsShooting { get; private set; }

    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        lastFiredTimestamp = Time.time;
    }

    void Update()
    {
        IsShooting = Input.GetButton("Fire");
        if (IsShooting && Time.time - lastFiredTimestamp >= FireRate)
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
}