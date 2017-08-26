using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public static readonly string CHANNEL_CURRENT_HEALTH = "BaseHealth.CurrentHealth";
    public static readonly string CHANNEL_INITIAL_HEALTH = "BaseHealth.InitialHealth";
    public static readonly string EVENT_DIED = "BaseHealth.Died";

    public float InitialHealth = 100;

    private DataProvider data;
    private float currentHealth;

	public bool destroyOnDeath = true;

    public bool IsDead
    {
        get { return currentHealth <= 0; }
    }

    public void Start()
    {
        currentHealth = InitialHealth;

        data = GetComponent<DataProvider>();
        if (data != null)
        {
            data.UpdateChannel(CHANNEL_INITIAL_HEALTH, InitialHealth);
            data.UpdateChannel(CHANNEL_CURRENT_HEALTH, currentHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!IsDead)
        {
            currentHealth -= amount;
            if (data != null)
            {
                data.UpdateChannel(CHANNEL_CURRENT_HEALTH, currentHealth);
            }

            if (IsDead)
            {
                Die();
                if (data != null)
                {
                    data.NotifyEvent(EVENT_DIED);
                }
            }
        }

    }

    public virtual void Die()
    {
		if (destroyOnDeath) {
			Destroy (gameObject);
		}
    }
}
