using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDelayController : Observable {

	public string FloatChannel;
	public GameObject WaveStartDelayText;
	public GameObject WaveStartDelayLable;


	public float DefaultFloat = -1;
	private float value;
	private bool textHidden = false;

	// Use this for initialization
	void Start () {
		if (WaveStartDelayText != null) {
			Bind<float> (FloatChannel, DefaultFloat, newValue => updateTextDisplay(newValue));
		}
	}

	private void updateTextDisplay(float newValue){
		value = newValue;
		if (textHidden) {
			if (value > 0) {
				textHidden = false;
				WaveStartDelayText.SetActive (true);
				WaveStartDelayLable.SetActive (true);

			}
		} else {
			if (value < 0) {
				textHidden = true;
				WaveStartDelayText.SetActive (false);
				WaveStartDelayLable.SetActive (false);

			}
		}
	}

	protected override void Render()
	{
		
	}
}
