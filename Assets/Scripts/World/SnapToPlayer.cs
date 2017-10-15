using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPlayer : MonoBehaviour
{
    private PlayerMovement player;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }
    }
}
