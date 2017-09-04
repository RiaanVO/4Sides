using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    public Renderer TargetRenderer;
    public Color LowIntensityColor;
    public Color HighIntensityColor;

    void Start()
    {
    }

    void Update()
    {
        float intensity = Mathf.Sin(Time.time);
        Color color = Color.Lerp(LowIntensityColor, HighIntensityColor, intensity);
        TargetRenderer.material.SetColor("_EmissionColor", color);
    }
}
