using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public Material IdleMaterial;
    public Material ActiveMaterial;
    public float TimeBetweenSpawns;
    public float TelegraphDuration;

    private Renderer render;
    private float spawnTime;

    void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;
        render.material = IdleMaterial;
        spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - spawnTime > TimeBetweenSpawns - TelegraphDuration)
        {
            StartCoroutine(Spawn());
            spawnTime = Time.time;
        }
    }

    IEnumerator Spawn()
    {
        render.material = ActiveMaterial;
        yield return new WaitForSeconds(TelegraphDuration);
        Instantiate(Enemy, transform.position, Quaternion.identity);
        render.material = IdleMaterial;
    }
}
