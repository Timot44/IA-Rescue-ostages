using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAParent : MonoBehaviour
{
    public float detectionRange;
    
    public float baseSpeed;
    
    public int baseDamage;
    
    protected int health;
    public int maxHealth;

    public virtual void SwitchToState(int stateIndex)
    {
             
    }
    
    public virtual void TakeDamage(int damage)
    {
        
    }

    public void Dead()
    {
        
    }
}
