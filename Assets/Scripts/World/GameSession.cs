using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameSession
{
    private static readonly Dictionary<string, string[]> SECTOR_SEQUENCE =
        new Dictionary<string, string[]>
    {
        {"1-A", new [] { "1-B" } },
        {"1-B", new [] { "1-C" } }
    };

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

    private static string currentSector;

    public static void StartSector(string id)
    {
        Score = 0;
        currentSector = id;
    }

    public static void NotifySectorCompleted(string id)
    {
        if (!string.IsNullOrEmpty(LastCompletedSector))
        {
            CompletedSectors.Add(LastCompletedSector);
        }
        LastCompletedSector = id;

        string[] nextSectors;
        if (SECTOR_SEQUENCE.TryGetValue(id, out nextSectors) &&
            nextSectors.Length > 0)
        {
            currentSector = nextSectors[0];
        }
    }

    public static string GetCurrentSectorScene()
    {
        if (string.IsNullOrEmpty(currentSector))
        {
            currentSector = SECTOR_SEQUENCE.ElementAt(0).Key;
        }
        return "Sector_" + currentSector;
    }
}
