using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : MonoBehaviour
{
   public Weapons pistol;
   
   
   
   
   
   
   
   private void OnTriggerEnter(Collider other1)
   {
      Debug.Log("Hello world");
      if (other1.gameObject.GetComponent<PlayerShoot>())
      {
         other1.gameObject.GetComponent<PlayerShoot>().currentWeapoons = pistol;
         gameObject.transform.SetParent(other1.gameObject.GetComponent<PlayerShoot>().weapons_holders);
         other1.gameObject.GetComponent<PlayerShoot>().SetUpCurrentWeapon();
         
         gameObject.transform.position = other1.gameObject.GetComponent<PlayerShoot>().weapons_holders.position;
         gameObject.transform.rotation = other1.gameObject.GetComponent<PlayerShoot>().weapons_holders.rotation;
         
         Destroy(gameObject.GetComponent<BoxCollider>());
      }
   }
}
