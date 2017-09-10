using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
	[Header("Score Settings")]
    public int ScoreToAdd = 100;

	[Header("Death Settings")]
	public GameObject explosion;
	public AudioClip deathSound;
	public AudioClip damageSound;
	public float deathDelayTime = 0f;


	private AudioSource audioSource;

	public override void Initialise ()
	{
		//explosion = GetComponentInChildren<ParticleSystem>();
		audioSource = GetComponent<AudioSource> ();
		base.Initialise ();
	}

	public override void KillSelf ()
	{
		playDeathSound ();
		//explosion.Play();
		Instantiate(explosion, transform.position, transform.rotation);
		StartCoroutine (deathDelay());
	}

    public override void Die()
    {
		playDeathSound ();
		//explosion.Play();
		Instantiate(explosion, transform.position, transform.rotation);

        var controller = GameObject.FindObjectOfType<GameController>();
        if (controller != null)
        {
            controller.AwardPoints(ScoreToAdd);
        }

		StartCoroutine (deathDelay());
    }

	public override void DamageSound(){
		if (audioSource != null && deathSound != null) {
			audioSource.PlayOneShot (damageSound);
		}
	}

	private IEnumerator deathDelay(){
		yield return new WaitForSeconds(deathDelayTime);
		KillGameobject ();
	}

	private void playDeathSound(){
		if (audioSource != null && deathSound != null) {
			audioSource.PlayOneShot (deathSound);
		}
	}


}
