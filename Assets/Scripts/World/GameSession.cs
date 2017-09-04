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

    private static List<string> completedSectors = new List<string>();
    public static List<string> CompletedSectors
    {
        get { return completedSectors; }
    }

    public static string LastCompletedSector { get; private set; }

    public static void StartNewLevel()
    {
        Score = 0;
    }

    public static void NotifySectorCompleted(string id)
    {
        if (!string.IsNullOrEmpty(LastCompletedSector))
        {
            CompletedSectors.Add(LastCompletedSector);
        }
        LastCompletedSector = id;
    }
}
