﻿using System;
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
    
    private void Awake()
    {
        playerLife = playerMaxLife;
        slider.maxValue = playerMaxLife;
        slider.value = playerMaxLife;
        
    }
    
    void Start()
    {
        
    }

  
    void Update()
    {
        if (playerLife <= 0)
        {
            //TODO Faire une function pour la mort du joueur//
        }
    }

    public void PlayerHeal(int amount)
    {
        playerLife += amount;
        slider.value = playerLife;
    }

    public void TakeDamage(int amount)
    {
        playerLife -= amount;
        slider.value = playerLife;
    }
}
