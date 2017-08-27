using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PooledObject
{
    private EnemyHealth health;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        if (health != null)
        {
            health.ResetHealth();
        }
    }
}
