using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MusicService;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private MusicPlayer _musicPlayer;
    private GameController _gameController; 

    public AudioClip _mouseOverSound;

	// Use this for initialization
	void Start () {
        _musicPlayer = MusicPlayer.Instance;
        _gameController = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResumeGame()
    {
        _gameController.ResumeGame();
    }

    public void ExitGame()
    {
        _gameController.ExitGame();
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
