﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

	public Vector3 offset;                     // The initial offset from the target.

    void Start()
    {
        if (target == null)
            return;

        // Calculate the initial offset.
		transform.position = target.position + offset;
        //offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        // Create a position the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(
            transform.position, targetCamPos,
            smoothing * Time.deltaTime);
    }
}
