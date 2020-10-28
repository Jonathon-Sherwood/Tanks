using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public HealthPowerup powerupData;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        PowerupManager test = other.gameObject.GetComponent<PowerupManager>();
        if(test != null)
        {
            powerupData.data = other.GetComponent<ShipData>();
            test.AddPowerup(powerupData);
            Destroy(gameObject);
        }
    }
}
