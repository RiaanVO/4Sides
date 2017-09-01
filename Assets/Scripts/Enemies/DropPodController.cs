using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody))]
public class DropPodController : PooledObject
{
    public float MinDropHeight = 15.0f;
    public float MaxDropHeight = 35.0f;

    private bool hasLanded;
    private Animator animator;
    private Rigidbody body;
    private EnemySpawner spawner;

    public void Initialize(Vector3 position)
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        spawner = GetComponentInChildren<EnemySpawner>();

        float dropHeight = Random.Range(MinDropHeight, MaxDropHeight);
        transform.position = position + (Vector3.up * dropHeight);

        hasLanded = false;
        body.isKinematic = false;
        animator.SetBool("HasLanded", false);
        spawner.Initialize();
        spawner.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasLanded)
        {
            return;
        }

        hasLanded = true;
        body.isKinematic = true;
        animator.SetBool("HasLanded", true);
    }

    public void StartSpawning()
    {
        spawner.enabled = true;
    }
}
