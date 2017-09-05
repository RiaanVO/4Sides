using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(DataProvider))]
[RequireComponent(typeof(AudioSource))]
public class MusicIntensity : MonoBehaviour
{
    public static readonly string CHANNEL_INTENSITY = "MusicIntensity.Intensity";

    [Range(0, 255)]
    public int Frequency = 0;
    public FFTWindow Window = FFTWindow.BlackmanHarris;

    private DataProvider data;
    private AudioSource source;
    private float[] spectrum = new float[256];

    void Start()
    {
        data = GetComponent<DataProvider>();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        source.GetSpectrumData(spectrum, 0, Window);
        data.UpdateChannel(CHANNEL_INTENSITY, spectrum[Frequency]);
    }
}
