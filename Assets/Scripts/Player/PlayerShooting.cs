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

    private float currentFireRate;


    public AudioClip laserSFX;
    public float volume = 1f;
    private AudioSource laserAudioSource;
    private bool isShootingLaser = false;
    private int laserDamage = 5;
    private float laserTickRate = 0.1f;
    private LineRenderer laserLine;
    private float laserLineWidth = 0.1f;
    private float laserTimer = 0f;
    private int wallLayerMask = 1 << 9;
    private int enemyLayerMask = 1 << 10;

    private bool usingPickup = false;

    public bool IsShooting { get; private set; }

    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        lastFiredTimestamp = Time.time;
        currentFireRate = FireRate;
        CurrentBullet = originBullet;

        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = laserLineWidth;
        laserLine.endWidth = laserLineWidth;
        laserLine.enabled = false;

        laserAudioSource = gameObject.AddComponent<AudioSource>();
        laserAudioSource.clip = laserSFX;
        laserAudioSource.volume = volume;
        laserAudioSource.playOnAwake = false;
        laserAudioSource.loop = true;
        laserAudioSource.spatialBlend = 0.75f;
    }

    void Update()
    {
        IsShooting = Input.GetButton("Fire");
        if (IsShooting) {

            if(Time.time - lastFiredTimestamp >= currentFireRate && !isShootingLaser){
              ShootBullet();
              if (MuzzleFlash != null)
              {
                fireSound.pitch = Random.Range(0.9f, 1.1f);
                fireSound.Play();
                MuzzleFlash.enabled = true;
              }
            }

            if(isShootingLaser){
              shootLaser();
            }

        } else if (MuzzleFlash != null) {
            MuzzleFlash.enabled = false;
            laserLine.enabled = false;
            laserAudioSource.Stop();
        }
    }

    void ShootBullet()
    {
        var bullet = CurrentBullet.GetPooledInstance<Bullet>();
        bullet.Initialize(shootingPoint.position, transform.rotation);
        lastFiredTimestamp = Time.time;
    }

    private void shootLaser(){
      laserLine.enabled = true;
      laserAudioSource.Play();

      //Draw the laser
      RaycastHit hit;
      float distance = 0f;
      if (Physics.Raycast(shootingPoint.position, transform.forward, out hit, 100, wallLayerMask))
      {
          distance = hit.distance;
          Vector3 endPoint = shootingPoint.localPosition + Vector3.forward * distance;
          laserLine.SetPosition(0, shootingPoint.localPosition);
          laserLine.SetPosition(1, endPoint);
      }

      //Get and damage colliders;
      laserTimer -= Time.deltaTime;
      if(laserTimer < 0){
        laserTimer = laserTickRate;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(shootingPoint.position, transform.forward, distance, enemyLayerMask);
        foreach(RaycastHit rayHit in hits){
          BaseHealth health = rayHit.collider.GetComponentInParent<BaseHealth>();
          if(health != null){
            health.TakeDamage(laserDamage);
          }
        }
      }

    }

    public void ChangeToLaserShooting(float duration, int damage, float tickRate){
      StartCoroutine (changeToLaserShooting (duration, damage, tickRate));
    }

    private IEnumerator changeToLaserShooting(float duration, int damage, float tickRate){
          usingPickup = true;
          isShootingLaser = true;
          laserDamage = damage;
          laserTickRate = tickRate;
          yield return new WaitForSeconds(duration);
  		    isShootingLaser = false;
          laserLine.enabled = false;
          usingPickup = false;
    }

    public void ChangeFireRate(float duration, float targetFireRate, Bullet newBullet)
    {
		    StartCoroutine (changeFireRate (duration, targetFireRate, newBullet));
    }

	private IEnumerator changeFireRate(float duration, float targetFireRate, Bullet newBullet)
    {
      usingPickup = true;

		currentFireRate = targetFireRate;
        if (newBullet != null)
            CurrentBullet = newBullet;
        yield return new WaitForSeconds(duration);
		    currentFireRate = FireRate;
        CurrentBullet = originBullet;
        usingPickup = false;

    }

    public bool isUsingPickup(){
      return usingPickup;
    }
}
