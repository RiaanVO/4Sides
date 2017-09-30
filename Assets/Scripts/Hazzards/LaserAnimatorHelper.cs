using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimatorHelper : MonoBehaviour {

	private LaserHazzardController laserHazzardController;

	// Use this for initialization
	void Start () {
		laserHazzardController = GetComponentInParent<LaserHazzardController> ();
	}

	public void StartCharging(){
		laserHazzardController.StartCharging ();
	}

	public void FireLaser(){
		laserHazzardController.FireLaser ();
	}
}
