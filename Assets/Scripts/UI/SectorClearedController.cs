using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorClearedController : MonoBehaviour
{
    public bool WasSuccessful = true;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void GoToMap()
    {
        var controller = GameObject.FindObjectOfType<GameController>();
        if (controller != null)
        {
            controller.GoToMapScreen(WasSuccessful);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (source != null)
        {
            source.clip = clip;
            source.Play();
        }
    }
}
