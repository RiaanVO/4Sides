using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody _body;

    [Header("Bullet Properties")]
    public float speed;
    public int damage;

    // Use this for initialization
    void Start () {
        _body = GetComponent<Rigidbody>();
        _body.transform.position += transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        _body.transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider colloder)
    {
        if (colloder.tag != "Bullet" && colloder.tag != "Player")
        Destroy(gameObject);
    }
}
