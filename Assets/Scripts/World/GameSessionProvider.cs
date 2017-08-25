using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataProvider))]
public class GameSessionProvider : MonoBehaviour
{
    public static readonly string CHANNEL_DISPLAY_SCORE = "GameSessionProvider.DisplayScore";

    private DataProvider data;

    void Start()
    {
        data = GetComponent<DataProvider>();
        data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);
    }
}
