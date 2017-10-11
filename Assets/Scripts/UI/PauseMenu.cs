using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private MusicPlayer player;
    private GameController controller;

    public AudioClip buttonClickSfx;

    void Start()
    {
        player = MusicPlayer.Instance;
        controller = FindObjectOfType<GameController>();
    }

    public void ResumeGame()
    {
        controller.ResumeGame();
        player.PlaySfx(buttonClickSfx);
    }

    public void ExitGame()
    {
        controller.ExitGame();
        player.PlaySfx(buttonClickSfx);
    }
}
