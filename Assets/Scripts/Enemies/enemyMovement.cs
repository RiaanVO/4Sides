using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour {

	public GameObject player;

	NavMeshAgent nav;
	Animator anim;


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();

	}

	void Update () {
		nav.SetDestination (player.transform.position);	

	}


	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			anim.SetBool ("Attacking", true);
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			nav.SetDestination (player.transform.position);	
			anim.SetBool ("Idling", false);
			anim.SetBool ("Attacking", false);
		}
	}

}
