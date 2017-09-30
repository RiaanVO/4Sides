using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour {

    private SceneNavigation _sceneNavigation;

    // Use this for initialization
    void Start () {
        _sceneNavigation = FindObjectOfType<SceneNavigation>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoBack()
    {
        _sceneNavigation.CloseHelpMenu();
    }
}
