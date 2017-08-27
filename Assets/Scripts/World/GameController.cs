using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DataProvider))]
public class GameController : Observable
{
    public static readonly string CHANNEL_DISPLAY_SCORE = "GameController.DisplayScore";

    public EventSource Player;

    private DataProvider data;

    void Start()
    {
        GameSession.StartNew();

        if (Player != null)
        {
            Player.Subscribe(BaseHealth.EVENT_DIED, GoToDeathScreen);
        }

        data = GetComponent<DataProvider>();
        data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);

        Bind<int>(PlayerScore.CHANNEL_SCORE, 0,
            value =>
            {
                GameSession.DisplayScore = value.ToString();
                data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);
            });
    }

    public void GoToDeathScreen()
    {
        SceneManager.LoadScene("DeathScene");
    }

    protected override void Render()
    {
    }
}
