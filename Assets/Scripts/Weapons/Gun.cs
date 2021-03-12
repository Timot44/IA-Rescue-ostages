using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   public Weapons weaponToAdd;
   
   
   
   
   
   
   
   private void OnTriggerStay(Collider other)
   {
     
      if (other.gameObject.GetComponent<PlayerShoot>())
      {
         //On set l'arme du joueur avec cette arme et on la tp au niveau du placeHolder, on setUp aussi les paramètres
         other.gameObject.GetComponent<PlayerShoot>().currentWeapoons = weaponToAdd;
         gameObject.transform.SetParent(other.gameObject.GetComponent<PlayerShoot>().weapons_holders);
         other.gameObject.GetComponent<PlayerShoot>().SetUpCurrentWeapon();
         
         gameObject.transform.position = other.gameObject.GetComponent<PlayerShoot>().weapons_holders.position;
         gameObject.transform.rotation = other.gameObject.GetComponent<PlayerShoot>().weapons_holders.rotation;
         
         Destroy(gameObject.GetComponent<BoxCollider>());
      }
   }
}
