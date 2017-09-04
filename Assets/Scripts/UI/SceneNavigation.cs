using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void GoToCurrentSector()
    {
        SceneManager.LoadScene(GameSession.GetCurrentSectorScene());
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
