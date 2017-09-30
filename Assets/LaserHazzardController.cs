using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHazzardController : MonoBehaviour {

	public Transform laserStartPoint;
	public Transform laserEndPoint;

	Animator laserAnimator;
	LineRenderer laserLine;
	public float laserLineWidth = 0.2f;

	public float chargeTime = 3f;
	public float fireTime = 2f;

	// Use this for initialization
	void Start () {
		laserAnimator = GetComponentInChildren<Animator> ();

		laserLine = GetComponent<LineRenderer> ();
		laserLine.startWidth = laserLineWidth;
		laserLine.endWidth = laserLineWidth;

		setLaserPositions ();
		laserLine.enabled = false;

		StartCharging ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void setLaserPositions(){
		laserLine.SetPosition (0, laserStartPoint.position);
		laserLine.SetPosition (1, laserEndPoint.position);
	}

	public void StartCharging(){
		StartCoroutine (chargeLaser (chargeTime));
	}

	public void FireLaser(){
		StartCoroutine (fireLaser (fireTime));
	}

	private IEnumerator chargeLaser(float currentChargeTime){
		//Debug.Log ("Charging laser");
		yield return new WaitForSeconds(currentChargeTime);
		//Debug.Log ("Opening laser");
		laserAnimator.SetTrigger ("Open");
	}

	private IEnumerator fireLaser(float currentFireTime){
		laserLine.enabled = true;
		//Debug.Log ("Firing laser");
		yield return new WaitForSeconds(currentFireTime);
		//Debug.Log ("Closing laser");
		laserLine.enabled = false;

		laserAnimator.SetTrigger ("Close");
	}
}
