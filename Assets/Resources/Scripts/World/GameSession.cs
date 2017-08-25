using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSession
{
    public static string DisplayScore { get; set; }

    public static void StartNew()
    {
        DisplayScore = "0";
    }
}
