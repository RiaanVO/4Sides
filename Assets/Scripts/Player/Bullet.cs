﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    public bool destroyOverDistance = true;
    public float MaximumRange = 15f;
    public float MaximumLifeTime = 10f;
    public float Speed = 4.0f;

    private GameObject player;
    private BaseHealth playerHealth;
    private Rigidbody body;
    private MeshRenderer mesh;
    private float createdAtTimestamp;

    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<BaseHealth>();
        body = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<MeshRenderer>();
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        createdAtTimestamp = Time.time;
    }

    void FixedUpdate()
    {
        body.transform.position += transform.forward * Time.deltaTime * Speed;

        if (Time.time - createdAtTimestamp > MaximumLifeTime)
        {
            ReturnToPool();
        }
        if (destroyOverDistance && !playerHealth.IsDead)
        {
            if (MaximumRange < Vector3.Distance(transform.position, player.transform.position))
            {
                ReturnToPool();
            }
        }
        
        mesh.material.SetTextureOffset("_MainTex",
            new Vector2(Time.time % 1, (Time.time * 4) % 1));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Solid")
        {
            ReturnToPool();
        }
    }
}