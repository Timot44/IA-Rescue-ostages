using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAHostage : MonoBehaviour
{
    private int health;
    public int maxHealth = 100;
    public float correctDistanceFromPlayer = 2f;

    private bool fleeingEnemies;
    private bool phaseTwo;
    private NavMeshAgent agent;
    private Transform player;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
    }

    public void SetStateToPhaseTwo()
    {
        phaseTwo = true;
    }

    public void Dead()
    {
        
    }

    public void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    public void Update()
    {
        if (phaseTwo)
        {
            if (Vector3.Distance(player.position, transform.position) > correctDistanceFromPlayer)
            {
            }
        }
    }
}
