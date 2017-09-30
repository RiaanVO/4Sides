using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunOverLava : MonoBehaviour {


	public float DelayToRegainSpeed = 2f;
	private NavMeshAgent enemyNavMeshAgent;



	void Start () {
		enemyNavMeshAgent = GetComponent<NavMeshAgent> ();
	}
	

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("LavaTrap")) {
			enemyNavMeshAgent.speed = 2f;
		}
	}


	void OnTriggerExit(Collider other){
		if(other.gameObject.CompareTag("LavaTrap"))
		{
			StartCoroutine (returnToNormalSpeed());
		}
	}

	IEnumerator returnToNormalSpeed () {
		yield return new WaitForSeconds (DelayToRegainSpeed);
		enemyNavMeshAgent.speed = 4.5f;
	}


}

