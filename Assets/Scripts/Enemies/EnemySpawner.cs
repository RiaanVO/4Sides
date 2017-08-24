using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public Material IdleMaterial;
    public Material ActiveMaterial;
    public float TelegraphDuration = 1.0f;

    private Renderer render;
    private bool isSpawning;
    private float telegraphStartTimestamp;

    void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;
        render.material = IdleMaterial;

        isSpawning = false;
        telegraphStartTimestamp = Time.time;
    }

    void Update()
    {
        if (isSpawning && Time.time - telegraphStartTimestamp > TelegraphDuration)
        {
            SpawnEnemy();
            isSpawning = false;
            render.material = IdleMaterial;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
    }

    public void Spawn()
    {
        render.material = ActiveMaterial;
        isSpawning = true;
        telegraphStartTimestamp = Time.time;
    }
}
