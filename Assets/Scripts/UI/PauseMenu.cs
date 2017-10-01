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

    public AudioClip buttonClickSfx;

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
        _musicPlayer.PlaySfx(buttonClickSfx);
    }

    public void ExitGame()
    {
        _gameController.ExitGame();
        _musicPlayer.PlaySfx(buttonClickSfx);
    }
}
