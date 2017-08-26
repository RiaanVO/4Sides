using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody _body;

	private float lifeTimer = 0f;
	private float maxLifeTime = 10f;

    [Header("Bullet Properties")]
    public float speed;
    //public int damage;

    // Use this for initialization
    void Start () {
        _body = GetComponent<Rigidbody>();
        _body.transform.position += transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        _body.transform.position += transform.forward * Time.deltaTime * speed;

		lifeTimer += Time.deltaTime;
		if (lifeTimer > maxLifeTime) {
			deactivateBullet ();
		}
    }


    private void OnTriggerEnter(Collider colloder)
    {
		if (colloder.tag != "Bullet" && colloder.tag != "Player") {
			deactivateBullet ();
		}
		
    }

	private void deactivateBullet(){
		//For object pooling it should set active to false
		Destroy(gameObject);
	}

}
/*
	 * if (other.gameObject.tag == "Bullet")
        {
            _damageTaken = other.transform.parent.gameObject.GetComponent<Bullet>().damage;
            myHealth.TakeDamage(_damageTaken);
        }
	 */