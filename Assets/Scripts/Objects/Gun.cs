﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   public Weapons weaponToAdd;
   
   private void OnTriggerEnter(Collider other)
   {
     
      if (other.gameObject.GetComponent<PlayerShoot>())
      {
         //On set l'arme du joueur avec cette arme et on la tp au niveau du placeHolder, on setUp aussi les paramètres de l'arme
         other.gameObject.GetComponent<PlayerShoot>().currentWeapoons = weaponToAdd;
         gameObject.transform.SetParent(other.gameObject.GetComponent<PlayerShoot>().weapons_holders);
         other.gameObject.GetComponent<PlayerShoot>().SetUpCurrentWeapon();
         //On set up la position et la rotation de l'arme par rapport au placeHolder
         gameObject.transform.position = other.gameObject.GetComponent<PlayerShoot>().weapons_holders.position;
         gameObject.transform.rotation = other.gameObject.GetComponent<PlayerShoot>().weapons_holders.rotation;
         
         Destroy(gameObject.GetComponent<BoxCollider>());
      }
   }
}