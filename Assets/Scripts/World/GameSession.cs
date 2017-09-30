using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameSession
{
    public static readonly Dictionary<string, string[]> SECTOR_DEPENDENCIES =
        new Dictionary<string, string[]>
    {
        {"1", new string[0] },

        {"2-A", new [] { "1" } },
        {"2-B", new [] { "1" } },
        {"2-C", new [] { "1" } },
        {"2-D", new [] { "1" } },

        {"3-AB", new [] { "2-A", "2-B" } },
        {"3-BC", new [] { "2-B", "2-C" } },
        {"3-CD", new [] { "2-C", "2-D" } },
        {"3-AD", new [] { "2-A", "2-D" } },

        {"4", new [] { "3-AB", "3-BC", "3-CD", "3-AD" } }
    };

    public static int EnemyLeft { get; set; }
    public static string DisplayScore
    {
        get { return EnemyLeft.ToString(); }
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
        EnemyLeft = 0;
        currentSector = id;
    }

    public static void NotifySectorCompleted(string id)
    {
        if (!string.IsNullOrEmpty(LastCompletedSector))
        {
            CompletedSectors.Add(LastCompletedSector);
        }
        LastCompletedSector = id;

        List<string> unlockedSectors = new List<string>();
        foreach (var sector in SECTOR_DEPENDENCIES)
        {
            if (sector.Key == id) continue;
            if (CompletedSectors.Contains(sector.Key)) continue;

            if (sector.Value.All(dependency => id == dependency ||
                CompletedSectors.Contains(dependency)))
            {
                unlockedSectors.Add(sector.Key);
            }
        }

        if (unlockedSectors.Count > 0)
        {
            currentSector = unlockedSectors[0];
        }
    }

    public static string GetCurrentSectorScene()
    {
        if (string.IsNullOrEmpty(currentSector))
        {
            currentSector = SECTOR_DEPENDENCIES.ElementAt(0).Key;
        }
        return "Sector_" + currentSector;
    }
}
