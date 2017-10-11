using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    private AudioSource source;

    private bool verified = false;
    private AudioClip queuedClip = null;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            verified = true;

            source = gameObject.AddComponent<AudioSource>();
            if (queuedClip != null)
            {
                source.clip = queuedClip;
                source.Play();
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySfx(AudioClip audio)
    {
        source.PlayOneShot(audio);
    }

    public void PlayAny(List<AudioClip> playlist, bool forceRestart)
    {
        var clip = playlist[Random.Range(0, playlist.Count)];
        if (!forceRestart && clip == source.clip) return;

        if (verified)
        {
            source.clip = clip;
            source.Play();
        }
        else
        {
            queuedClip = clip;
        }
    }
}
