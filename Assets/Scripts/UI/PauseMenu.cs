using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameController controller;

    void Start()
    {
        controller = FindObjectOfType<GameController>();
    }

    public void ResumeGame()
    {
        controller.ResumeGame();
    }

    public void ExitGame()
    {
        controller.ExitGame();
    }
}
