using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    public int ScoreToAdd = 100;

    public override void Die()
    {
        var controller = GameObject.FindObjectOfType<GameController>();
        if (controller != null)
        {
            controller.AwardPoints(ScoreToAdd);
        }

        base.Die();
    }
}
