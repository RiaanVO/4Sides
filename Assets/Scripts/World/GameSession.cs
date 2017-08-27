using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSession
{
    public static int Score { get; set; }
    public static string DisplayScore
    {
        get { return Score.ToString(); }
    }

    public static void StartNew()
    {
        Score = 0;
    }
}
