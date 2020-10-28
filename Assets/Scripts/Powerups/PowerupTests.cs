using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTests : MonoBehaviour
{
    public PowerupManager powerupManager;

    // Start is called before the first frame update
    void Start()
    {
        powerupManager = GetComponent<PowerupManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            HealthPowerup newHealthPowerup = new HealthPowerup();
            newHealthPowerup.amount = 100;
            newHealthPowerup.data = GetComponent<ShipData>();
            newHealthPowerup.isInfinite = true;
            powerupManager.AddPowerup(newHealthPowerup);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            powerupManager.RemovePowerup(powerupManager.powerups[0]);
        }
    }
}
