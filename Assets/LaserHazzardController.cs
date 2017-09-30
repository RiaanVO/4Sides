using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHazzardController : MonoBehaviour {

	[Header("Duration Settings")]
	public float startDelay = 0f;
	public float chargeTime = 3f;
	public float fireTime = 2f;

	[Header("Base Settings")]
	public Transform laserStartPoint;
	public Transform laserEndPoint;
	public float laserLineWidth = 0.1f;

	Animator laserAnimator;
	LineRenderer laserLine;

	BoxCollider laserDamageBox;

	// Use this for initialization
	void Start () {
		laserAnimator = GetComponentInChildren<Animator> ();

		//Calculate the position of the endpoint

		//Vector3 facing

		RaycastHit hit;
		if (Physics.Raycast (laserStartPoint.position, transform.right, out hit)) {
			laserEndPoint.position = laserStartPoint.position + transform.right * hit.distance;
		}

		laserDamageBox = gameObject.AddComponent<BoxCollider> ();
		laserDamageBox.center = new Vector3 ();
		laserDamageBox.size = new Vector3 ();

		laserLine = GetComponent<LineRenderer> ();	
		laserLine.startWidth = laserLineWidth;
		laserLine.endWidth = laserLineWidth;

		setLaserPositions ();
		laserLine.enabled = false;

		Invoke ("StartCharging", startDelay);
		//StartCharging ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void setLaserPositions(){
		laserLine.SetPosition (0, laserStartPoint.localPosition);
		laserLine.SetPosition (1, laserEndPoint.localPosition);
	}

	public void StartCharging(){
		StartCoroutine (chargeLaser (chargeTime));
	}

	public void FireLaser(){
		StartCoroutine (fireLaser (fireTime));
	}

	private IEnumerator chargeLaser(float currentChargeTime){
		yield return new WaitForSeconds(currentChargeTime);

		laserAnimator.SetTrigger ("Open");
	}

	private IEnumerator fireLaser(float currentFireTime){
		laserLine.enabled = true;

		yield return new WaitForSeconds(currentFireTime);

		laserLine.enabled = false;
		laserAnimator.SetTrigger ("Close");
	}
}
