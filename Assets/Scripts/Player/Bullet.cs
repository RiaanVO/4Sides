using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    public float MaximumLifeTime = 2f;
    public float Speed = 4.0f;

    private Rigidbody body;
    private MeshRenderer mesh;
    private float createdAtTimestamp;

    void Start()
    {
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
