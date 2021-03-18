using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammos : MonoBehaviour
{

    public int numAmmoToAdd = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerShoot>().currentWeapoons)
        {
            other.gameObject.GetComponent<PlayerShoot>().ammo += numAmmoToAdd;
            Destroy(gameObject);
        }
    }
}
