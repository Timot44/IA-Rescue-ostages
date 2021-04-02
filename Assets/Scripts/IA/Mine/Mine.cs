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
        // if the player enter the collision of this object, the mine explode
        if (other.CompareTag("Player"))
        {
            // Reset the bool in the patroller to say that it can place a new mine
            aIPlacer.minePlaced = false;
            IAHostage hostage = FindObjectOfType<IAHostage>();
            PlayerLife player = FindObjectOfType<PlayerLife>();

            // Check distance from player and Hostage to see if it can damage them
            if (Vector3.Distance(hostage.transform.position, transform.position) < damageDistance)
            {
                hostage.TakeDamage(damageAmount);
            }
            
            if (Vector3.Distance(player.transform.position, transform.position) < damageDistance)
            {
                player.TakeDamage(damageAmount);
            }
            
            // Deactivate text to desarme
            gameObject.GetComponent<DisarmMine>().DesactiveText();
            
            // Spawn particle and destroy game object
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
