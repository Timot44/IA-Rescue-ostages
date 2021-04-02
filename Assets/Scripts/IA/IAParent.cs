using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class IAParent : MonoBehaviour
{
    [Header("Main Parameters")]
    public int baseDamage;
    
    [Header("Life parameters")]
    public int health;
    public int maxHealth;
    public Slider slider;
    
    [Header("Movement Parameters")]
    public float distanceToChangeWaypoint;
    public Transform[] patrolWaypoint;
    public bool isAttack;
    
    [Header("Detection Parameters")]
    public float detectionRange = 10;
    public float angle = 90;
    public int rayCount = 5;
    
        public virtual void SwitchToState()
    {
    }

        // Initialize void for help in child
    public virtual void HelpPatroller(Transform playerTransform)
    {
    }
    
    public virtual void TakeDamage(int damage)
    {
        // Part to handle damage and death
        health -= damage;
        slider.value = health;
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        // Destroy the IA
        Destroy(gameObject);
    }

 

    public void SetBarMax(int amount)
    {
        // Set slider to show life
        slider.maxValue = amount;
        slider.value = amount;
    }
}
