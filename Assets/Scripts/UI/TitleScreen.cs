using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

    public bool clearGameMemory = true;

	// Use this for initialization
	void Start () {
        if (clearGameMemory)
            GameSession.ClearProgress();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
