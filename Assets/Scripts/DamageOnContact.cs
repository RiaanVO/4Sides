using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour {
	public int damageToDeal = 10;
	public bool dieOnContact = false;

	public void OnTriggerEnter(Collider other){
		Debug.Log ("Hit collider");
		BaseHealth otherHealth = other.gameObject.GetComponentInParent<BaseHealth> ();
		if (otherHealth != null) {
			otherHealth.TakeDamage (damageToDeal);

			if (dieOnContact) {
				BaseHealth health = gameObject.GetComponent<BaseHealth> ();
				health.Die ();
			}
		}
	}
}
