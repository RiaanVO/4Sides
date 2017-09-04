using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : Observable
{
    public Renderer TargetRenderer;
    public Color LowIntensityColor;
    public Color HighIntensityColor;
    public string IntensityChannel;

    private Material material;
    private float intensity;

    void Awake()
    {
        material = TargetRenderer.material;
    }

    public void Bind(DataProvider provider)
    {
        Provider = provider;
        Bind<float>(IntensityChannel, 0.0f, newValue => intensity = newValue);
    }

    protected override void Render()
    {
        Color color = Color.Lerp(LowIntensityColor, HighIntensityColor, intensity);
        material.SetColor("_EmissionColor", color);
    }
}
