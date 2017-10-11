using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> Playlist;
    public bool ForceRestart = false;

    void Start()
    {
        if (Playlist == null || Playlist.Count == 0) return;

        var player = GameObject.FindObjectOfType<MusicPlayer>();
        if (player != null)
        {
            player.PlayAny(Playlist, ForceRestart);
        }
    }
}
