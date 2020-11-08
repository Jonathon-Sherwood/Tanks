using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainshot : Pickup
{
    public ChainshotDebuff powerupData;

    public override void Update()
    {
        base.Update();
    }



    public override void OnTriggerEnter(Collider other)
    {
        PowerupManager test = other.gameObject.GetComponent<PowerupManager>();
        if (test != null)
        {
            powerupData.data = other.GetComponent<ShipData>();
            test.AddPowerup(powerupData);
            Destroy(gameObject);
        }
    }

}
