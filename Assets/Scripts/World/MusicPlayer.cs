using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicService
{
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer instance = null;
        public static MusicPlayer Instance
        {
            get { return instance; }
        }

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

        private AudioSource audioSource;

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlaySfx(AudioClip audio)
        {
            audioSource.PlayOneShot(audio);
        }
    }
}
