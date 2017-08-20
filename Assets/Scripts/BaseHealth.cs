using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour {

	[SerializeField]
	private int maxHealth = 100;
	[SerializeField]
	private int currentHealth = 0;

	public int MaxHealth {
		get { return maxHealth; }
	}

	public int CurrentHealth {
		get { return currentHealth; }
	}

	public bool IsDead {
		get { return currentHealth == 0; }
	}

	public void Start() {
		Initialise();
	}

	public void Initialise() {
		SetFullHealth();
	}

	public void TakeDamage(int amount) {
		if (!IsDead) {
			if ((currentHealth -= amount) <= 0) {
				currentHealth = 0;
				Die ();
			}
			Debug.Log(gameObject.name + " damaged");
		}
	}

	public void Die() {
		Destroy(gameObject);
	}

	public void SetFullHealth() {
		currentHealth = maxHealth;
	}
}
