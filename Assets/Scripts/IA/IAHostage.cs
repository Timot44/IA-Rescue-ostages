using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAHostage : MonoBehaviour
{
    public int health;
    private int maxHealth;

    public void SwitchToState(int stateIndex)
    {
        
    }

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
        
    }

    public void Dead()
    {
        
    }
}
