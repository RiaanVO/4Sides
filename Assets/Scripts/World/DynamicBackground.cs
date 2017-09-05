using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicBackground : Observable
{
    public Renderer TargetRenderer;
    public Color LowIntensityColor;
    public Color HighIntensityColor;
    public string IntensityChannel;
    public float MinThreshold = 0.01f;
    public float IntensityMultiplier = 3.0f;
    public float IntensityDecay = 0.2f;

    private Material material;
    private float rawIntensity;
    private Color bgColor;

    void Awake()
    {
        material = TargetRenderer.material;
        bgColor = material.GetColor("_EmissionColor");
    }

    public void Bind(DataProvider provider)
    {
        Provider = provider;
        Bind<float>(IntensityChannel, 0.0f, newValue => rawIntensity = newValue);
    }

    void FixedUpdate()
    {
        if (rawIntensity > MinThreshold)
        {
            bgColor = Color.Lerp(bgColor, HighIntensityColor,
                (rawIntensity - MinThreshold) * IntensityMultiplier);
        }
        else
        {
            bgColor = Color.Lerp(bgColor, LowIntensityColor, IntensityDecay);
        }
        material.SetColor("_EmissionColor", bgColor);
    }

    protected override void Render()
    {
    }
}
