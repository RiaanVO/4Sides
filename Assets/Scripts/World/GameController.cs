using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

[RequireComponent(typeof(DataProvider))]
public class GameController : MonoBehaviour
{
    public static readonly string CHANNEL_SCORE = "GameController.Score";
    public static readonly string CHANNEL_DISPLAY_SCORE = "GameController.DisplayScore";

    public EventSource Player;
    public EventSource SpawnManager;

    [Header("Score Settings")]
    public AudioClip scoreAddSFX;
    private AudioSource audioSource;

    private DataProvider data;

    void Start()
    {
        GameSession.StartNew();

        data = GetComponent<DataProvider>();
        data.UpdateChannel(CHANNEL_SCORE, GameSession.Score);
        data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);

        if (Player != null)
        {
            Player.Subscribe(BaseHealth.EVENT_DIED, GoToDeathScreen);
        }
        if (SpawnManager != null)
        {
            SpawnManager.Subscribe(EnemySpawnManager.EVENT_ALL_WAVES_CLEARED, GoToTitleScreen);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void AwardPoints(int points)
    {
        GameSession.Score += points;
        data.UpdateChannel(CHANNEL_SCORE, GameSession.Score);
        data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);

        if (audioSource != null && scoreAddSFX != null)
        {
            audioSource.pitch = Random.Range(0.97f, 1.03f);
            audioSource.PlayOneShot(scoreAddSFX);
        }
    }

    private void GoToDeathScreen(EventSource source, string eventName)
    {
        SceneManager.LoadScene("DeathScene");
    }

    private void GoToTitleScreen(EventSource source, string eventName)
    {
        SceneManager.LoadScene("TitleScene");
    }
}
