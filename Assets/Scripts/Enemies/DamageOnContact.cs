using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int DamageToDeal = 10;
    public bool SelfDestructOnContact = false;
    public List<string> TagsToCollideWith;

    private BaseHealth myHealth;

    public void Start()
    {
        myHealth = GetComponent<BaseHealth>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (TagsToCollideWith == null || TagsToCollideWith.Count == 0 ||
            !TagsToCollideWith.Contains(other.gameObject.tag))
            return;

        var otherHealth = other.GetComponentInParent<BaseHealth>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(DamageToDeal);
            if (SelfDestructOnContact && myHealth != null)
            {
                myHealth.Die();
            }
        }
    }
}
