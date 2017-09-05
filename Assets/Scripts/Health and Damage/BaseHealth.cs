using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public static readonly string CHANNEL_CURRENT_HEALTH = "BaseHealth.CurrentHealth";
    public static readonly string CHANNEL_INITIAL_HEALTH = "BaseHealth.InitialHealth";
    public static readonly string EVENT_DIED = "BaseHealth.Died";

	private DataProvider data;
	private EventSource events;
	private PooledObject poolable;
	private float currentHealth;

	[Header("Base Health Settings")]
    public float InitialHealth = 100;
    public bool DestroyOnDeath = true;

	[Header("On Damage Settings")]
	public bool showDamageFlashEffects = true;
	public float damageFlashDuration = 0.2f;
	public int flashesPerDuration = 4;
	private float flashTimer = 0f;
	private bool flashActive = false;

	public Material flashMaterial;
	private Material baseMaterial;
	public Renderer modelRenderer;

	public bool showDamageText = true;
	public DamageTextController DamageText;

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

        events = GetComponent<EventSource>();
        poolable = GetComponent<PooledObject>();


		//Get the materials for the flashing
		if(modelRenderer != null){
			baseMaterial = modelRenderer.material;
		}
		flashTimer = damageFlashDuration;

		Initialise ();
    }

	public virtual void Initialise(){}

	public void Update(){
		if (showDamageFlashEffects) {
			if (flashActive) {
				bool showBaseMaterial = false;

				float t = Mathf.Sin (360 * (flashTimer / damageFlashDuration) * (float)flashesPerDuration);
				showBaseMaterial = t > 0;

				if (showBaseMaterial) {
					SetRenderMaterial (baseMaterial);
				} else {
					SetRenderMaterial (flashMaterial);
				}

				flashTimer += Time.deltaTime;
				if (flashTimer > damageFlashDuration) {
					flashActive = false;

					SetRenderMaterial (baseMaterial);
				}
			}
		}
		
	}

	private void SetRenderMaterial(Material material){
		if(modelRenderer != null && material != null){
			modelRenderer.material = material;
		}
	}

	private void ShowDamageEffects (int damageAmount){
		flashActive = true;
		flashTimer = 0f;

		if (showDamageText) {
			DamageTextController.CreateDamageText (damageAmount, gameObject.transform, DamageText);
		}

	}

    public void ResetHealth()
    {
        currentHealth = InitialHealth;
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

			ShowDamageEffects (amount);

            if (data != null)
            {
                data.UpdateChannel(CHANNEL_CURRENT_HEALTH, currentHealth);
            }

            if (IsDead)
            {
                Die();
            }
        }
    }

	public void Heal(int amount){
		if (!IsDead)
		{
			currentHealth += amount;
			if (currentHealth > InitialHealth) {
				currentHealth = InitialHealth;
			}

			if (data != null)
			{
				data.UpdateChannel(CHANNEL_CURRENT_HEALTH, currentHealth);
			}
		}
	}

	public virtual void KillSelf(){
		KillGameobject ();
	}

    public virtual void Die()
    {
		KillGameobject ();
    }

	public void KillGameobject(){
		if (events != null)
		{
			events.Notify(EVENT_DIED);
		}
		if (DestroyOnDeath)
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
	}
}
