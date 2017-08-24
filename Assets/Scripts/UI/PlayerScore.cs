using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static readonly string CHANNEL_SCORE = "PlayerScore.Score";
    public static readonly string CHANNEL_DISPLAY_SCORE = "PlayerScore.DisplayScore";

    private int score;
    private DataProvider data;

    void Start()
    {
        score = 0;

        data = GetComponent<DataProvider>();
        if (data != null)
        {
            data.UpdateChannel(CHANNEL_SCORE, score);
        }
    }

    void Update()
    {
        // TODO: Remove this once score is awarded from killing enemies
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AwardPoints(100);
        }
    }

    public void AwardPoints(int points)
    {
        score += points;
        if (data != null)
        {
            data.UpdateChannel(CHANNEL_SCORE, score);
            data.UpdateChannel(CHANNEL_DISPLAY_SCORE, score.ToString());
        }
    }
}

