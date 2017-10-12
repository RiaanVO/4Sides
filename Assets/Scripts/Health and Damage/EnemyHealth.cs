using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    [Header("EnemyLeft Settings")]
    public int ScoreToAdd = 100;

    [Header("Death Settings")]
    public GameObject explosion;
    public List<AudioClip> deathSounds;
    public AudioClip damageSound;
    public float deathDelayTime = 0f;

    private AudioSource damagedSoundSource;
    private AudioSource deathSoundSource;

    public override void Initialise()
    {
        if (damagedSoundSource == null && damageSound != null)
        {
            damagedSoundSource = AddAudioSource(damageSound, 0.5f);
        }
        if (deathSoundSource == null && deathSounds != null && deathSounds.Count > 0)
        {
            deathSoundSource = AddAudioSource(deathSounds[0], 1.0f);
        }

        //explosion = GetComponentInChildren<ParticleSystem>();
        base.Initialise();
    }

    public override void KillSelf()
    {
        playDeathSound();
        //explosion.Play();
        Instantiate(explosion, transform.position, transform.rotation);
        StartCoroutine(deathDelay());
    }

    public override void Die()
    {
        playDeathSound();
        //explosion.Play();
        Instantiate(explosion, transform.position, transform.rotation);
        StartCoroutine(deathDelay());
    }

    public override void DamageSound()
    {
        if (damagedSoundSource != null)
        {
            damagedSoundSource.pitch = Random.Range(0.8f, 1.2f);
            damagedSoundSource.Stop();
            damagedSoundSource.Play();
        }
    }

    private IEnumerator deathDelay()
    {
        yield return new WaitForSeconds(deathDelayTime);
        KillGameobject();
    }

    private void playDeathSound()
    {
        if (deathSoundSource != null)
        {
            deathSoundSource.clip = deathSounds[Random.Range(0, deathSounds.Count)];
            // deathSoundSource.Play();
        }
    }

    private AudioSource AddAudioSource(AudioClip clip, float volume)
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.playOnAwake = false;
        source.spatialBlend = 0.75f;
        return source;
    }
}
