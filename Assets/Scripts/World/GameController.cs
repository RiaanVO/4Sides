using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Observable
{
    public EventSource Player;

    void Start()
    {
        GameSession.StartNew();

        if (Player != null)
        {
            Player.Subscribe(BaseHealth.EVENT_DIED, GoToDeathScreen);
        }

        Bind<string>(PlayerScore.CHANNEL_DISPLAY_SCORE, "0",
            value => GameSession.DisplayScore = value);
    }

    public void GoToDeathScreen()
    {
        SceneManager.LoadScene("DeathScene");
    }

    protected override void Render()
    {
    }
}
