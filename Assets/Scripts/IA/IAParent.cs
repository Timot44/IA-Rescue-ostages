using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAParent : MonoBehaviour
{
    [Header("Main Parameters")]
    public float detectionRange;
    
    public int baseDamage;
    
    protected int health;
    public int maxHealth;
    
    [Header("Movement Parameters")]
    public float baseSpeed;
    public float distanceToChangeWaypoint;
    public Transform[] patrolWaypoint;

    public abstract void SwitchToState(int stateIndex);
    
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void Start()
    {
        health = maxHealth;
    }
}
