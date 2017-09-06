using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextController : PooledObject {

	//https://www.youtube.com/watch?v=fbUOG7f3jq8

	private static GameObject canvas;

	private Animator animator;
	private Text displayText;
	private float duration;

	private bool isActive;
	private Vector3 worldPosition;


	public static void CreateDamageText(int damage, Transform location, DamageTextController DamageText){
		if (DamageText == null)
			return;
		var damageTextInstance = DamageText.GetPooledInstance <DamageTextController> ();
		damageTextInstance.Initilize (damage, location);
	}

	public void OnEnable(){
		if (animator == null) {
			animator = GetComponentInChildren<Animator> ();
		}
		if (displayText == null) {
			displayText = GetComponentInChildren<Text> ();
		}
		if (canvas == null) {
			canvas = GameObject.Find ("Canvas");
		}
		isActive = true;

		AnimatorClipInfo[] animatorClipInfo = animator.GetCurrentAnimatorClipInfo (0);
		duration = animatorClipInfo [0].clip.length;
		StartCoroutine (removeText());
	}

	public void Initilize(int damage, Transform location){
		//set the parent to the canvas
		gameObject.transform.SetParent (canvas.transform, false);
		worldPosition = location.position;
		setTextLocation (worldPosition);
		SetDamageText (damage);
	}

	public void Update(){
		if (isActive) {
			setTextLocation (worldPosition);
		}
	}

	public void SetDamageText(int damage){
		displayText.text = damage + "";
	}

	private IEnumerator removeText(){
		yield return new WaitForSeconds(duration);
		ReturnToPool ();
	}

	private void setTextLocation(Vector3 position){
		Vector2 screenPosition = Camera.main.WorldToScreenPoint (position);
		gameObject.transform.position = screenPosition;
	}
}
