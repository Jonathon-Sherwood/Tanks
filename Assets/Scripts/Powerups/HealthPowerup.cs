﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public TakeDamage health;


    public override void OnPickup()
    {
        //Adds adjustable health to the ship
        data.health.currentHealth += amount;
        base.OnPickup();
    }


    public override void OnExpire()
    {
        //removes adjustable health to the ship
        data.health.currentHealth -= amount;
        base.OnExpire();
    }
}
