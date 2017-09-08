using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {
	Transform[] children;
	Transform player;

	public float heightOffset = -1f;
	private bool currentActiveState = false;

	// Use this for initialization
	void Awake () {
		children = gameObject.GetComponentsInChildren<Transform> ();
		//Debug.Log (gameObject.name + " number of children: " + children.Length);
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		bool newState = isCoveringPlayer ();
		if (currentActiveState != newState) {
			currentActiveState = newState;
			setChildrenVisable (!newState);
		}
	}

	private bool isCoveringPlayer(){
		if (player == null)
			return true;
		return transform.position.y + heightOffset > player.position.y;
	}

	private void setChildrenVisable(bool active){
		foreach (Transform child in children) {
			Renderer renderer = child.gameObject.GetComponent<Renderer> ();
			if (renderer != null) {
				renderer.enabled = active;
			}
			//child.gameObject (active);
		}
	}
}
