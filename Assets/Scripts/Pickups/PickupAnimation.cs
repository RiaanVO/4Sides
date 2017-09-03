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
		bottomPoint = new Vector3 (transform.position.x, transform.position.y - heightShift, transform.position.z);
		topPoint = new Vector3 (transform.position.x, transform.position.y + heightShift, transform.position.z);
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
