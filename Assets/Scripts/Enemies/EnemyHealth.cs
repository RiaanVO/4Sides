using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public int ScoreToAdd = 100;

    public override void Die()
    {
        var playerScore = GameObject.FindObjectOfType<PlayerScore>();
        if (playerScore != null)
        {
            playerScore.AwardPoints(ScoreToAdd);
        }

        base.Die();
    }
}
