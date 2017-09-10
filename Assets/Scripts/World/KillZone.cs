using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	public void OnTriggerEnter(Collider other){
		BaseHealth health = other.GetComponentInParent<BaseHealth> ();
		if (health != null) {
			Debug.Log ("Killing " + other.gameObject.name);
			health.KillSelf ();
		}
	}
}
