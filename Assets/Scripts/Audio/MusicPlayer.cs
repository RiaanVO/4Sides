using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    public int MaxClipsToRemember = 10;
    public float CrossfadeTime = 1.0f;
    public AudioSource Source1;
    public AudioSource Source2;
    public AudioMixerSnapshot Source1Snapshot;
    public AudioMixerSnapshot Source2Snapshot;

    private bool usingSource1 = false;
    private bool verified = false;
    private AudioClip queuedClip = null;
    private List<String> memory = new List<String>();

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

            if (Source1 == null || Source2 == null)
            {
                Debug.LogError("Audio sources not configured!");
            }
            if (Source1Snapshot == null || Source2Snapshot == null)
            {
                Debug.LogError("Audio source snapshots not configured!");
            }

            if (queuedClip != null)
            {
                PlayClip(queuedClip);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayAny(List<AudioClip> playlist, bool forceRestart)
    {
        if (playlist == null || playlist.Count == 0) return;

        AudioClip clip = playlist[0];
        if (playlist.Count > 1)
        {
            // filter to least played tracks
            var byNumTimesPlayed =
                from track in playlist
                group track by memory.Count(m => track.name == m) into grp
                orderby grp.Key
                select grp;
            var leastPlayedTracks = byNumTimesPlayed.First();

            if (leastPlayedTracks.Count() == 1)
            {
                // one track has been played the least, pick that one
                clip = leastPlayedTracks.First();
            }
            else if (leastPlayedTracks.Key == 0)
            {
                // some tracks have not been played in recent memory
                // pick a random one
                clip = leastPlayedTracks.ElementAt(
                    Random.Range(0, leastPlayedTracks.Count()));
            }
            else
            {
                // all tracks have been played at least once in recent memory
                // filter to tracks that have the longest time since played
                var byTimeSincePlayed =
                    from track in leastPlayedTracks
                    group track by memory.LastIndexOf(track.name) into grp
                    orderby grp.Key
                    select grp;

                // pick a random clip
                var longestTimeSincePlayed = byTimeSincePlayed.First();
                clip = longestTimeSincePlayed.ElementAt(
                    Random.Range(0, longestTimeSincePlayed.Count()));

            }
        }

        var currentSource = usingSource1 ? Source1 : Source2;
        if (!forceRestart && clip == currentSource.clip) return;

        if (verified)
        {
            PlayClip(clip);
        }
        else
        {
            queuedClip = clip;
        }
    }

    private void PlayClip(AudioClip clip)
    {
        var source = Source1;
        var targetSnapshot = Source1Snapshot;
        if (usingSource1)
        {
            source = Source2;
            targetSnapshot = Source2Snapshot;
        }

        source.clip = clip;
        source.loop = true;
        source.Play();
        targetSnapshot.TransitionTo(CrossfadeTime);

        if (MaxClipsToRemember > 0)
        {
            memory.Add(clip.name);
            while (memory.Count > MaxClipsToRemember)
            {
                memory.RemoveAt(0);
            }
        }

        usingSource1 = !usingSource1;
    }
}
