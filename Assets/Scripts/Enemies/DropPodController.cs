using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody))]
public class DropPodController : PooledObject
{
    public float DropHeight = 25.0f;

    private bool hasLanded;
    private Animator animator;
    private Rigidbody body;

    public void Initialize(Vector3 position)
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

        transform.position = position + (Vector3.up * DropHeight);
        hasLanded = false;
        body.isKinematic = false;
        animator.SetBool("HasLanded", false);
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
}
