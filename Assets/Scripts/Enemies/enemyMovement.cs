using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    PlayerMovement player;
    NavMeshAgent nav;
    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Attacking", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nav.SetDestination(player.transform.position);
            anim.SetBool("Idling", false);
            anim.SetBool("Attacking", false);
        }
    }
}
