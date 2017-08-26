using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth {

	public int scoreToAdd = 100;

	public override void Die (){
		PlayerScore playerScore = GameObject.FindObjectOfType<PlayerScore>();
		if (playerScore != null) {
			playerScore.AwardPoints (scoreToAdd);
		}
		Destroy (gameObject);
	}
}
