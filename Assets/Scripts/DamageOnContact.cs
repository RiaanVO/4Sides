using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damageToDeal = 10;
    public bool dieOnContact = false;

    public string[] tagsToHit;

    public void OnTriggerEnter(Collider other)
    {
        if (tagsToHit != null)
        {
            bool correctTag = false;
            foreach (string tag in tagsToHit)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    correctTag = true;
                    break;
                }
            }
            if (!correctTag)
                return;
        }

        BaseHealth otherHealth = other.gameObject.GetComponentInParent<BaseHealth>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageToDeal);

            if (dieOnContact)
            {
                BaseHealth health = gameObject.GetComponent<BaseHealth>();
                health.Die();
            }
        }
    }
}
