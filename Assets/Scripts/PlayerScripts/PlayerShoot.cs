using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Player
{
    public Weapons currentWeapoons;
    
    private Vector3 _pos_cam;
    private Quaternion _rot_cam;
    private Vector3 _cam_shootpoint;

    public Transform weapons_holders;

    [Header("Current Weapon")]
    public float fireRate;
    public int weaponDamage;
    public int ammo;
    // Start is called before the first frame update
    private void Awake()
    {
        weapon = currentWeapoons;
        shootRate = fireRate;
        currentAmmo = ammo;
        damageWeapon = weaponDamage;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapoons != null)
        {
            currentWeapoons.fireRate -= Time.deltaTime;

            currentWeapoons.fireRate = Mathf.Clamp(currentWeapoons.fireRate, 0, Mathf.Infinity);
        }
       


        if (Input.GetMouseButtonDown(0) && currentWeapoons.fireRate <= 0f && currentWeapoons != null)
        {
            Shoot();
        }
    }

    public void SetUpCurrentWeapon()
    {
        //Set up les parametres pour l'arme actuel
        fireRate = currentWeapoons.fireRate;
        ammo = currentWeapoons.currentAmmo;
        weaponDamage = currentWeapoons.damage;

    }
    private void FixedUpdate()
    {
        //Update the pos and the rot of the camera so that the pistol's shoot going better
        _pos_cam = Camera.main.transform.position;
        _rot_cam = Camera.main.transform.rotation;
        _cam_shootpoint = Camera.main.transform.forward;
    }

    public override void Shoot()
    {
        RaycastHit raycastHit;
        ammo--;
        Debug.DrawRay(_pos_cam, Camera.main.transform.forward * 100, Color.magenta, 3f);

        fireRate = currentWeapoons.maxFireRate;

         
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, currentWeapoons.range,currentWeapoons.layers))
        {
            Debug.Log(raycastHit.collider.name);
         
        }
    }
    

    
}
