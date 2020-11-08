using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Powerup
{
    public float amount = 100;
    public float lifespan = 5;
    public int shots = 1;
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
