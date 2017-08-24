using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

	public Text scoreText;

	private float Score;

	void Start () {
		Score = 0;
		UpdateScore ();
	}



	void Update () {
		// add ENEMY IS KILLED here instead of Space bar
		if (Input.GetKeyDown (KeyCode.Space))  {
			Score = Score + 100;
			UpdateScore ();
		}
	}
		
	void UpdateScore (){
		scoreText.text = "SCORE: " + Score.ToString ("0");
	}


}

