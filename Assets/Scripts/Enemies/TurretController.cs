﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public float detectionRadius;
    public float timeBetweenShot;
    public Bullet Bullet;
    public Material normalMaterials;
    public Material attackingMaterial;
    public Renderer gunRender;

    private AudioSource source;
    private PlayerMovement _player;
    private double _timer;
    private bool _gunReady;

    // Use this for initialization
    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        ResetTimer();
        _gunReady = false;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            if (PlayerInRange())
            {
                WarmGun();
                Aiming();
                _timer += Time.deltaTime;
                if (_timer >= timeBetweenShot)
                {
                    Shoot();
                    ResetTimer();
                }
            }
            else
            {
                CoolGun();
            }
        }
    }

    private void Shoot()
    {
        var bullet = Bullet.GetPooledInstance<Bullet>();
        bullet.Initialize(transform.position, transform.rotation);
        if (source != null)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
            source.Play();
        }
    }

    private bool PlayerInRange()
    {
        bool inRange = false;
        if (_player != null)
        {
            if (detectionRadius > Vector3.Distance(transform.position, _player.transform.position))
            {
                inRange = true;
            }
        }
        return inRange;
    }

    private void WarmGun()
    {
        if (!_gunReady)
        {
            gunRender.sharedMaterial = attackingMaterial;
            _gunReady = true;
        }
    }

    private void CoolGun()
    {
        if (_gunReady)
        {
            gunRender.sharedMaterial = normalMaterials;
            _gunReady = false;
        }
    }

    private void Aiming()
    {
        Vector3 position = _player.transform.position;
        //position.y = 0;
        transform.LookAt(position);
    }

    private void ResetTimer()
    {
        _timer = 0;
    }
}
