using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProgressBar : Observable
{
    public string ValueChannel;
    public string MaxValueChannel;
    public float DefaultValue = 0.5f;
    public float DefaultMaxValue = 1.0f;

    private Image image;
    private float value;
    private float maxValue;

	public bool useChangeRate = true;
	public float changeRate = 0.1f;
	private float currentValue;
	private bool valueChanged = false;
	private float direction;

	public bool useColourChange = true;
	[SerializeField]
	public Color fullColour;
	[SerializeField]
	public Color lowColour;


    void Start()
    {
        image = GetComponent<Image>();

		Bind<float>(ValueChannel, DefaultValue, newValue => setNewValue(newValue));
		Bind<float>(MaxValueChannel, DefaultMaxValue, newValue => setNewMax(newValue));
    }

	private void setNewMax(float newValue){
		maxValue = newValue;
		value = maxValue;
		currentValue = maxValue;
		Render ();
	}

	private void setNewValue (float newValue){
		value = newValue;
		direction = newValue < currentValue ? -1 : 1;
		valueChanged = true;

		if (!useChangeRate) {
			currentValue = value;
		}
	}

	void Update(){
		if(useChangeRate){
			if (valueChanged) {
				currentValue += direction * changeRate * Time.deltaTime;
				if (currentValue < value && direction < 0 || currentValue > value && direction > 0) {
					currentValue = value;
					valueChanged = false;
				}
				Render ();
			}
		}
	}

    protected override void Render()
    {
		float fillAmount = currentValue / maxValue;
        image.fillAmount = fillAmount;
		image.color = Color.Lerp (lowColour, fullColour, fillAmount);
    }
}
