using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DataProvider))]
public class GameController : MonoBehaviour
{
    public static readonly string CHANNEL_SCORE = "GameController.Score";
    public static readonly string CHANNEL_DISPLAY_SCORE = "GameController.DisplayScore";

    public EventSource Player;

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
    }

    public void AwardPoints(int points)
    {
        GameSession.Score += points;
        data.UpdateChannel(CHANNEL_SCORE, GameSession.Score);
        data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);
    }

    private void GoToDeathScreen(EventSource source, string eventName)
    {
        SceneManager.LoadScene("DeathScene");
    }
}
