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
    private PooledObject poolable;

    public void Start()
    {
        myHealth = GetComponent<BaseHealth>();
        poolable = GetComponent<PooledObject>();
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
            if (SelfDestructOnContact)
            {
                SelfDestruct();
            }
        }
    }

    private void SelfDestruct()
    {
        if (myHealth == null)
        {
            if (poolable == null)
            {
                Destroy(gameObject);
            }
            else
            {
                poolable.ReturnToPool();
            }
        }
        else
        {
            myHealth.Die();
        }
    }
}
