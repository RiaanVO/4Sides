using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float BaseShake = 0.01f;

    public void DirectionalShake(Vector3 direction, float amount)
    {
        transform.Translate(
            direction.x * BaseShake * amount,
            direction.y * BaseShake * amount,
            direction.z * BaseShake * amount);
    }

    public void RandomShake(float amount)
    {
        DirectionalShake(new Vector3(
            Mathf.Sign(Random.Range(-1, 1)),
            Mathf.Sign(Random.Range(-1, 1)),
            Mathf.Sign(Random.Range(-1, 1))), amount);
    }
}
