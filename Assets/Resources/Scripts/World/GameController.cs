using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Observable
{
    void Start()
    {
        GameSession.StartNew();

        Bind<string>(PlayerScore.CHANNEL_DISPLAY_SCORE, "0",
            value => GameSession.DisplayScore = value);
        Subscribe(BaseHealth.EVENT_DIED, GoToDeathScreen);
    }

    public void GoToDeathScreen()
    {
        SceneManager.LoadScene("DeathScene");
    }

    protected override void Render()
    {
    }
}
