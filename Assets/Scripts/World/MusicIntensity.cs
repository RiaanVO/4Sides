using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataProvider))]
[RequireComponent(typeof(AudioSource))]
public class MusicIntensity : MonoBehaviour
{
    public static readonly string CHANNEL_INTENSITY = "MusicIntensity.Intensity";

    private DataProvider data;
    private AudioSource source;

    void Start()
    {
        data = GetComponent<DataProvider>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        data.UpdateChannel(CHANNEL_INTENSITY, Mathf.Sin(Time.time));
    }
}
