using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHazzardController : MonoBehaviour
{

    [Header("Duration Settings")]
    public float startDelay = 0f;
    public float chargeTime = 3f;
    public float fireTime = 2f;

    [Header("Base Settings")]
    public Transform laserStartPoint;
    public Transform laserEndPoint;
    public float laserLineWidth = 0.1f;

    Animator laserAnimator;
    LineRenderer laserLine;

    BoxCollider laserDamageBox;
    DamageOverTime damgOverTime;

    [Header("Audio Settings")]
    public float volume = 0.1f;
    public AudioClip laserIdle;
    public AudioClip laserTurnOn;
    public AudioClip laserOn;
    public AudioClip laserTurnOff;

    AudioSource idleSource;
    AudioSource onOffSource;
    AudioSource onSource;

    // Use this for initialization
    void Start()
    {
        laserAnimator = GetComponentInChildren<Animator>();

        //Set up the line renderer
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = laserLineWidth;
        laserLine.endWidth = laserLineWidth;
        laserLine.enabled = false;

        //Set up the trigger used for damage over time
        laserDamageBox = gameObject.AddComponent<BoxCollider>();
        laserDamageBox.enabled = false;

        //Determine how long the laser should be
        RaycastHit hit;
        if (Physics.Raycast(laserStartPoint.position, transform.right, out hit))
        {
            laserEndPoint.position = laserStartPoint.position + transform.right * hit.distance;
            setLaserPositions();

            laserDamageBox.isTrigger = true;
            laserDamageBox.center = new Vector3(hit.distance / 2, 0, 0);
            laserDamageBox.size = new Vector3(hit.distance, 1, 0.2f);
        }

        damgOverTime = GetComponent<DamageOverTime>();

        /*
        idleSource = gameObject.AddComponent<AudioSource>();
        idleSource.volume = volume;
        idleSource.clip = laserIdle;
        idleSource.loop = true;
        idleSource.spatialBlend = 1.0f;
        idleSource.Play();

        onOffSource = gameObject.AddComponent<AudioSource>();
        onOffSource.loop = false;
        onOffSource.volume = volume;
        onOffSource.spatialBlend = 1.0f;

        onSource = gameObject.AddComponent<AudioSource>();
        onSource.loop = true;
        onSource.clip = laserOn;
        onSource.volume = volume;
        onSource.spatialBlend = 1.0f;
        onSource.Stop();
		*/

        Invoke("StartCharging", startDelay);
    }

    private void setLaserPositions()
    {
        laserLine.SetPosition(0, laserStartPoint.localPosition);
        laserLine.SetPosition(1, laserEndPoint.localPosition);
    }

    public void StartCharging()
    {
        StartCoroutine(chargeLaser(chargeTime));
    }

    public void FireLaser()
    {
        StartCoroutine(fireLaser(fireTime));
    }

    private IEnumerator chargeLaser(float currentChargeTime)
    {
        // idleSource.Play();
        yield return new WaitForSeconds(currentChargeTime);

        laserAnimator.SetTrigger("Open");
        // idleSource.Stop();
        // onOffSource.clip = laserTurnOn;
        // onOffSource.Play();
    }

    private IEnumerator fireLaser(float currentFireTime)
    {
        // onSource.Play();

        laserLine.enabled = true;
        laserDamageBox.enabled = true;

        yield return new WaitForSeconds(currentFireTime);

        // onSource.Stop();
        laserLine.enabled = false;
        laserDamageBox.enabled = false;
        damgOverTime.clearAllHealths();

        laserAnimator.SetTrigger("Close");
        // onOffSource.clip = laserTurnOff;
        // onOffSource.Play();

    }
}
