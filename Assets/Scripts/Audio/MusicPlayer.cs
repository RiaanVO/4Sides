using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    public int MaxClipsToRemember = 10;

    private AudioSource source;
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

            source = gameObject.GetComponent<AudioSource>();
            if (queuedClip != null)
            {
                PlayClip(queuedClip);
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

        if (!forceRestart && clip == source.clip) return;

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
        source.clip = clip;
        source.loop = true;
        source.Play();

        if (MaxClipsToRemember > 0)
        {
            memory.Add(clip.name);
            while (memory.Count > MaxClipsToRemember)
            {
                memory.RemoveAt(0);
            }
        }
    }
}
