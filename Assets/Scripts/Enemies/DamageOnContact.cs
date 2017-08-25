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
    private Bullet _bullet;
    private int _damageTaken;

    public void Start()
    {
        myHealth = GetComponent<BaseHealth>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            _damageTaken = other.transform.parent.gameObject.GetComponent<Bullet>().damage;
            myHealth.TakeDamage(_damageTaken);
        }
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
