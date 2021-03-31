using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : Player
{

    [Header("LIFE Parameters")] 
    public int playerLife;
    public int playerMaxLife;
    public Slider slider;

    public GameManager gameManager;
    public GameObject go_Hostage;
    
  public  void Start()
    {
        playerLife = playerMaxLife;
        slider.maxValue = playerMaxLife;
        slider.value = playerMaxLife;
    }

  
    void Update()
    {
        if (playerLife <= 0)
        { 
            gameManager.RespawnPlayer(gameObject, go_Hostage);
        }
        
        
    }

    public void PlayerHeal(int amount)
    {
        
        playerLife += amount;
        if (playerLife > playerMaxLife)
        {
            playerLife = playerMaxLife;
        }
        slider.value = playerLife;
        
    }

    public void TakeDamage(int amount)
    {
        playerLife -= amount;
        slider.value = playerLife;
    }
}
