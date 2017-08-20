using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {


	public float TotalHealth = 100f;
	public float CurrentHealth;

	void Start ()
	{
		CurrentHealth = TotalHealth;
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			if (CurrentHealth > 0) 
			{
				TakeDamage ();
			} 

			else if (CurrentHealth <= 0)
			{
				CurrentHealth = 0;
			}
		}
	}

	void TakeDamage() {
		CurrentHealth -= 10;
		transform.localScale = new Vector3((CurrentHealth / TotalHealth), 1, 1);
	}
}
