using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public IAPatroller aIPlacer;
    public float damageDistance;
    public int damageAmount;
    public GameObject explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            aIPlacer.minePlaced = false;
            IAHostage hostage = FindObjectOfType<IAHostage>();
            PlayerLife player = FindObjectOfType<PlayerLife>();

            if (Vector3.Distance(hostage.transform.position, transform.position) < damageDistance)
            {
                hostage.TakeDamage(damageAmount);
            }
            
            if (Vector3.Distance(player.transform.position, transform.position) < damageDistance)
            {
                player.TakeDamage(damageAmount);
            }

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
