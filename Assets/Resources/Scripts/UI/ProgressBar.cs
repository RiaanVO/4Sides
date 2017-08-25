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

    void Start()
    {
        image = GetComponent<Image>();

        Bind<float>(ValueChannel, DefaultValue, newValue => value = newValue);
        Bind<float>(MaxValueChannel, DefaultMaxValue, newValue => maxValue = newValue);
    }

    protected override void Render()
    {
        image.fillAmount = value / maxValue;
    }
}
