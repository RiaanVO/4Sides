using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    PlayerMovement player;
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = Object.FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (player != null)
        {
            nav.SetDestination(player.transform.position);
        }
    }
}
