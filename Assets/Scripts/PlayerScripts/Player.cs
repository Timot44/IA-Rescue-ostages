using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public int life;
    public int maxLife;
    public float walkSpeed;
    public int damageWeapon;
    public float shootRate;
    public int ammoCount;
    public bool canDisarm;
    public Weapons weapon;
    public int currentAmmo;




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
