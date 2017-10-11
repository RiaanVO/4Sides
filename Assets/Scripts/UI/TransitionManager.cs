using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class TransitionManager : MonoBehaviour
{
    private static TransitionManager instance = null;
    public static TransitionManager Instance
    {
        get { return instance; }
    }

    private Animator animator;
    private AudioSource source;

    private string destinationScene;
    private AsyncOperation loadScene;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void Navigate(string destinationScene)
    {
        this.destinationScene = destinationScene;
        animator.SetTrigger("StartTransition");
    }

    private void OnNavigationCompleted()
    {
        StartCoroutine("Load");
    }

    IEnumerator Load()
    {
        loadScene = SceneManager.LoadSceneAsync(destinationScene);
        yield return loadScene;
    }

    void Update()
    {
        if (loadScene != null && loadScene.isDone)
        {
            loadScene = null;
            animator.SetTrigger("FinishTransition");
        }
    }

    public void PlayTransitionSound()
    {
        source.Play();
    }
}
