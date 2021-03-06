﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainshotPickup : Pickup
{
    public ChainshotPowerup powerupData;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other) //Adds this powerup to the list on pickup
    {
        PowerupManager test = other.gameObject.GetComponent<PowerupManager>();
        if (test != null)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, GameManager.instance.sfxAudio);
            powerupData.data = other.GetComponent<ShipData>();
            test.AddPowerup(powerupData);
            Destroy(gameObject);
        }
    }
}
