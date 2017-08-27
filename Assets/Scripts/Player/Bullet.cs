using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    public float MaximumLifeTime = 10f;
    public float Speed = 4.0f;

    private Rigidbody body;
    private float createdAtTimestamp;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        createdAtTimestamp = Time.time;
    }

    void Update()
    {
        body.transform.position += transform.forward * Time.deltaTime * Speed;

        if (Time.time - createdAtTimestamp > MaximumLifeTime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Bullet" && collider.tag != "Player")
        {
            ReturnToPool();
        }
    }
}