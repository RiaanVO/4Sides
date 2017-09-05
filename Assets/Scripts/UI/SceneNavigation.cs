using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void GoToCurrentSector()
    {
        GoToScene(GameSession.GetCurrentSectorScene());
    }

    public void GoToTitleScreen()
    {
        GoToScene("TitleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
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
}
