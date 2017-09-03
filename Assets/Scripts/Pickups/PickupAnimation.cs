using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation: MonoBehaviour {

	public bool shouldRotate = true;
	public bool shouldBounce = true;

	[Header("Animation Settings")]
	public float heightShift = 1f;
	public float bounceSpeed = 1f;
	public float rotationSpeed = 1f;

	private Vector3 bottomPoint;
	private Vector3 topPoint;

	private float rotationAngle = 0f;
	private float bounceAngle = 0f;

	// Use this for initialization
	void Start () {
		SetBouncePositions (transform.position);
	}

	public void SetBouncePositions(Vector3 newPosition){
		bottomPoint = new Vector3 (newPosition.x, newPosition.y - heightShift, newPosition.z);
		topPoint = new Vector3 (newPosition.x, newPosition.y + heightShift, newPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldRotate) {
			rotationAngle += Time.deltaTime * rotationSpeed;
			transform.rotation = Quaternion.Euler (0, rotationAngle, 0);
		}

		if (shouldBounce) {
			bounceAngle += Time.deltaTime * bounceSpeed;
			float t = (Mathf.Sin (bounceAngle) + 1) / 2;
			transform.position = Vector3.Lerp (bottomPoint, topPoint, t);
		}
	}
}
