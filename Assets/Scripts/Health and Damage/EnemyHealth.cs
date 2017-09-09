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
	public float deathDelayTime = 0f;

	private AudioSource audioSource;

	public override void Initialise ()
	{
		audioSource = GetComponent<AudioSource> ();
		base.Initialise ();
	}

	public override void KillSelf ()
	{
		playDeathSound ();
		StartCoroutine (deathDelay());
	}

    public override void Die()
    {
		playDeathSound ();

        var controller = GameObject.FindObjectOfType<GameController>();
        if (controller != null)
        {
            controller.AwardPoints(ScoreToAdd);
        }

		StartCoroutine (deathDelay());
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
