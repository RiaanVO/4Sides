using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int DamageToDeal = 10;
    public float explosionRadius = 5;
    public bool SelfDestructOnContact = false;
    public bool isExplosive = false;
    public List<string> TagsToCollideWith;

    public GameObject explosion;

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
        if (isExplosive)
        {
            ExplosionDamage(gameObject.transform.position);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
        else
        {
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
    }

    void ExplosionDamage(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, explosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (TagsToCollideWith.Contains(hitColliders[i].gameObject.tag))
            {
                var otherHealth = hitColliders[i].GetComponentInParent<BaseHealth>();
                if (otherHealth != null)
                    otherHealth.TakeDamage(DamageToDeal);
            }
        }
        if (SelfDestructOnContact)
            SelfDestruct();
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
            myHealth.KillSelf();
        }
    }
}
