using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour {

	public List<string> tagsToDamage;

	[Header("Damage Settings")]
	public int damageToDeal = 1;
	public float damageTickRate = 0.1f;

	private float timer = 0f;

	private List<BaseHealth> currentHealths;
	private List<BaseHealth> newHealths;
	private List<BaseHealth> removeHealths;

	[Header("Audio Settings")]
	private AudioSource audioSource;
	public AudioClip damageSFX;

	public void Start(){
		audioSource = GetComponent<AudioSource> ();

		currentHealths = new List<BaseHealth> ();
		newHealths = new List<BaseHealth> ();
		removeHealths = new List<BaseHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (newHealths.Count > 0) {
			currentHealths.AddRange (newHealths);
			newHealths.Clear ();
		}

		timer += Time.deltaTime;
		if (timer > damageTickRate) {
			timer = 0f;
			if (currentHealths.Count > 0) {
				foreach (BaseHealth baseHealth in currentHealths) {
					if (baseHealth != null) {
						baseHealth.TakeDamage (damageToDeal);
					}
				}

				if (audioSource != null && damageSFX != null) {
					audioSource.PlayOneShot (damageSFX);
				}
			}
		}

		if (removeHealths.Count > 0) {
			currentHealths.RemoveAll (t => removeHealths.Contains(t));
			removeHealths.Clear ();
		}
	}

	public void OnTriggerEnter(Collider other){
		handleCollision (other, true);
	}

	public void OnTriggerExit(Collider other){
		handleCollision (other, false);
	}

	private void handleCollision(Collider other, bool isEntering){
		if (tagsToDamage == null)
			return;
		
		if(tagsToDamage.Contains(other.gameObject.tag)){ //other.gameObject.CompareTag(PLAYER_TAG)
			BaseHealth health = other.gameObject.GetComponentInParent<BaseHealth>();
			if(health != null){
				if (isEntering) {
					newHealths.Add(health);
				} else {
					removeHealths.Add(health);
				}
			}
		}
	}

	public void clearAllHealths(){
		removeHealths.AddRange (currentHealths);
	}
}
