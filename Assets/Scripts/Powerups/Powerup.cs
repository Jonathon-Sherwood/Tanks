﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Powerup
{
    //List of adjustable elements for pickups
    public float amount = 100;
    public float lifespan = 5;
    public bool isInfinite;
    public AudioClip soundEffect;
    public AudioClip expire;
    public GameObject particle;
    public ShipData data;

    public virtual void OnPickup()
    {

    }

    public virtual void OnExpire()
    {

    }
}
