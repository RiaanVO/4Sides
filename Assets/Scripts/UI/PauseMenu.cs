using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MusicService;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private MusicPlayer _musicPlayer;
    private SceneNavigation _sceneNavigation;

    private GameObject _resumeButton,
                       _exitButton;

    public AudioClip _mouseOverSound;

	// Use this for initialization
	void Start () {
        _musicPlayer = MusicPlayer.Instance;
        _sceneNavigation = gameObject.AddComponent<SceneNavigation>();
        _resumeButton = gameObject.transform.Find("ResumeButton").gameObject;
        _exitButton = gameObject.transform.Find("ExitButton").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("PauseScene");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        _sceneNavigation.GoToTitleScreen();
    }

    public void PlayMouseOverSound()
    {
        if (_musicPlayer != null)
        {
            _musicPlayer.PlaySfx(_mouseOverSound);
        }
    }

    public void MouseOver()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);
        PlayMouseOverSound();
    }
}
