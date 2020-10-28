using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager :  MonoBehaviour
{
    public List<Powerup> powerups;

    private void Update()
    {
        HandlePowerupExpiration();
    }

    public void HandlePowerupExpiration()
    {
        foreach(Powerup pu in powerups)
        {
            pu.lifespan -= Time.deltaTime;

            if(pu.lifespan <= 0)
            {
                RemovePowerup(pu);
            }
        }
    }

    public void AddPowerup(Powerup powerupToAdd)
    {
        powerupToAdd.OnPickup();
        powerups.Add(powerupToAdd);
    }

    public void RemovePowerup(Powerup powerupToRemove)
    {
        powerupToRemove.OnExpire();
        powerups.Remove(powerupToRemove);
    }
}
