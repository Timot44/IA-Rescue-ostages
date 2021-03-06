using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public string weaponName;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI countdownToShootAgain;
    [Header("Effect")]
    public ParticleSystem shoot_vfx;
    public GameObject impact_vfx;
    void Update()
    {
        if (currentWeapoons)
        {
            fireRate -= Time.deltaTime;
            fireRate = Mathf.Clamp(fireRate, 0, Mathf.Infinity);
        }

        countdownToShootAgain.text = $"Shoot In: {fireRate.ToString("F")}";

        if (Input.GetMouseButtonDown(0) && fireRate <= 0f && currentWeapoons && ammo > 0)
        {
            Shoot();
        }
        
        
    }

    public void SetUpCurrentWeapon()
    {
        //Set up the parameters of the current weapon
        fireRate = currentWeapoons.fireRate;
        ammo = currentWeapoons.currentAmmo;
        weaponDamage = currentWeapoons.damage;
        weaponName = currentWeapoons.name;
        weaponNameText.text = weaponName;
        ammoText.text = ammo.ToString("0");
    }
    
    private void FixedUpdate()
    {
        //Update the pos and rot of the camera
        _pos_cam = Camera.main.transform.position;
        _rot_cam = Camera.main.transform.rotation;
        _cam_shootpoint = Camera.main.transform.forward;
    }

    public void UpdateTextAmmo()
    {
        ammoText.text = ammo.ToString("0");
    }

    private void Shoot()
    {
        RaycastHit raycastHit;
        ammo--;
        Debug.DrawRay(_pos_cam, Camera.main.transform.forward * 100, Color.magenta, 3f);
        UpdateTextAmmo();
        shoot_vfx.Play();
        
        
        //If the raycast touch an object with the good layers he trigger the function TakeDamage for the IA   
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, currentWeapoons.range,currentWeapoons.layers))
        {
            GameObject impact = Instantiate(impact_vfx, raycastHit.point, Quaternion.LookRotation(gameObject.transform.position));
            if (raycastHit.collider.gameObject.GetComponent<IAParent>())
            {
                raycastHit.collider.gameObject.GetComponent<IAParent>().TakeDamage(weaponDamage);
            }
           
            Destroy(impact, 2);
        }
        fireRate = currentWeapoons.fireRate;
    }

    
    

    
}
