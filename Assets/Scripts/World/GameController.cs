using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

[RequireComponent(typeof(DataProvider))]
public class GameController : MonoBehaviour
{
    public static readonly string CHANNEL_SCORE = "GameController.EnemyLeft";
    public static readonly string CHANNEL_DISPLAY_SCORE = "GameController.DisplayScore";
    public static readonly string CHANNEL_SECTOR = "GameController.Sector";

    public string Sector = "N-X";
    public EventSource Player;
    public EventSource SpawnManager;

    [Header("EnemyLeft Settings")]
    public AudioClip scoreAddSFX;
    private AudioSource audioSource;

    private DataProvider data;

    private bool _paused;

    void Start()
    {
        GameSession.StartSector(Sector);

        data = GetComponent<DataProvider>();
        data.UpdateChannel(CHANNEL_SCORE, GameSession.EnemyLeft);
        data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);
        data.UpdateChannel(CHANNEL_SECTOR, Sector);

        if (Player != null)
        {
            Player.Subscribe(BaseHealth.EVENT_DIED, GoToDeathScreen);
        }
        if (SpawnManager != null)
        {
            SpawnManager.Subscribe(EnemySpawnManager.EVENT_ALL_WAVES_CLEARED, GoToMapScreen);
        }

        audioSource = GetComponent<AudioSource>();

        _paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!_paused)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _paused = true;
        SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        StartCoroutine(ClosePauseMenu());
        Time.timeScale = 1;
        _paused = false;
    }

    public void ExitGame()
    {
        StartCoroutine(ClosePauseMenu());
        GoToScene("TitleScene");
        Time.timeScale = 1;
    }

    public void UpdateEnemyNumber(int number)
    {
        GameSession.EnemyLeft = number;
        data.UpdateChannel(CHANNEL_SCORE, GameSession.EnemyLeft);
        data.UpdateChannel(CHANNEL_DISPLAY_SCORE, GameSession.DisplayScore);

        //if (audioSource != null && scoreAddSFX != null)
        //{
        //    audioSource.pitch = Random.Range(0.97f, 1.03f);
        //    audioSource.PlayOneShot(scoreAddSFX);
        //}
    }

    private void GoToDeathScreen(EventSource source, string eventName)
    {
        GoToScene("DeathScene");
    }

    private void GoToMapScreen(EventSource source, string eventName)
    {
        if (!string.IsNullOrEmpty(Sector))
        {
            GameSession.NotifySectorCompleted(Sector);
        }
        GoToScene("MapScene");
    }

    private void GoToScene(string name)
    {
        var transitions = GameObject.FindObjectOfType<TransitionManager>();
        if (transitions == null)
        {
            SceneManager.LoadScene(name);
        }
        else
        {
            transitions.Navigate(name);
        }
    }

    IEnumerator ClosePauseMenu()
    {
        SceneManager.UnloadSceneAsync("PauseScene");
        yield return null;
    }
}
