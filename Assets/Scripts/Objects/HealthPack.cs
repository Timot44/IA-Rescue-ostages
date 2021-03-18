using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healthToAdd = 10;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerLife>().playerLife < other.gameObject.GetComponent<PlayerLife>().playerMaxLife)
        {
            other.gameObject.GetComponent<PlayerLife>().PlayerHeal(healthToAdd);
            Destroy(gameObject);
        }
    }
}
