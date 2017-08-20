using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMovement : MonoBehaviour {

	public float moveSpeed = 0.1f;
	public float rotationSpeed = 8f;

	private Transform seekTarget;
	public bool shouldSeekTarget;

	private float seekStopRadius = 0.2f;


	// Use this for initialization
	void Start () {
		seekTarget = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
	}

	void FixedUpdate(){
		if (seekTarget == null || !shouldSeekTarget)
			return;

		//Move towards the target
		Vector3 moveDirection = seekTarget.position - transform.position;

		if (moveDirection.sqrMagnitude > seekStopRadius * seekStopRadius) {
			transform.position += moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
		}
		//Face the target
		Quaternion newRotation = Quaternion.LookRotation(moveDirection.normalized);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.fixedDeltaTime * rotationSpeed);
	}
}
