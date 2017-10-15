using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSound : MonoBehaviour
{
    public float TimeToWait = 1.0f;
    public AudioSource Source;
    public List<AudioClip> Clips;

    private float playTimestamp;

    void Start()
    {
        playTimestamp = Time.time - TimeToWait;
    }

    void Update()
    {
        if (Source == null || Clips == null || Clips.Count == 0) return;

        if (Time.time - playTimestamp > TimeToWait)
        {
            Source.clip = Clips[Random.Range(0, Clips.Count)];
            Source.Play();
            playTimestamp = Time.time;
        }
    }
}
