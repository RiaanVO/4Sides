using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisabilityController : MonoBehaviour
{

	Transform[] children;
	Transform player;

	public float heightOffset = -1f;
	public bool currentActiveState = false;

	// Use this for initialization
	void Awake ()
	{
		children = gameObject.GetComponentsInChildren<Transform> ();
		//Debug.Log (gameObject.name + " number of children: " + children.Length);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update ()
	{
		bool newState = isCoveringPlayer ();
		if (currentActiveState != newState) {
			currentActiveState = newState;
			setChildrenVisable (!newState);
		}
	}

	private bool isCoveringPlayer ()
	{
		if (player == null)
			return true;
		return transform.position.y + heightOffset > player.position.y;
	}

	private void setChildrenVisable (bool active)
	{
		foreach (Transform child in children) {
			Renderer renderer = child.gameObject.GetComponent<Renderer> ();
			if (renderer != null) {
				SetMaterialVisable (renderer, active);
				/*
				if (active) {
					SetMaterialOpaque (renderer);
				} else {
					SetMaterialTransparent (renderer);
				}
				*/
			}
			/*
			if (renderer != null) {
				//renderer.enabled = active;
				Color tempColour = renderer.material.color;
				if (active) {
					tempColour.a = 1f;
				} else {
					tempColour.a = 0.01f;
				}
				renderer.material.color = tempColour;

			}

			//child.gameObject (active);
			*/
		}
	}

	private void SetMaterialVisable(Renderer renderer, bool isVisable){
		//Debug.Log (renderer.gameObject.name + " num materials: " + renderer.materials.Length);
		foreach (Material m in renderer.materials) {
			Color tempColour = renderer.material.color;
			if (isVisable) {
				tempColour.a = 1f;
			} else {
				tempColour.a = 0.05f;
			}
			renderer.material.color = tempColour;
		}
	}



	//Not working right now!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	//https://www.youtube.com/watch?v=nNjNWDZSkAI



	private void SetMaterialTransparent (Renderer renderer){
		foreach (Material m in renderer.materials) {
			m.SetFloat ("_Mode", 2);
			m.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			m.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			m.SetInt ("_ZWrite", 0);
			m.DisableKeyword ("_ALPHATEST_ON");
			m.EnableKeyword ("_ALPHABLEND_ON");
			m.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
			m.renderQueue = 3000;
		}
	}



	private void SetMaterialOpaque (Renderer renderer){
		foreach (Material m in renderer.materials) {
			m.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			m.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			m.SetInt ("_ZWrite", 1);
			m.DisableKeyword ("_ALPHATEST_ON");
			m.DisableKeyword ("_ALPHABLEND_ON");
			m.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
			m.renderQueue = -1;
		}
	}
}
