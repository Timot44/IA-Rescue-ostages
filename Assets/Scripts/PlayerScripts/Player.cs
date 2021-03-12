using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected int life;
    protected int maxLife;
    protected float walkSpeed;
    protected int damageWeapon;
    protected float shootRate;
    protected int ammoCount;
    protected bool canDisarm;
    protected Weapons weapon;
    protected int currentAmmo;




    public virtual void Movement()
    {
        
    }


    public virtual void Shoot()
    {
        
    }
    
    public virtual void TakeDamage()
    {
        
    }
}
