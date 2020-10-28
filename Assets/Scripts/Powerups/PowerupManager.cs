using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager :  MonoBehaviour
{
    public List<Powerup> powerups;
    private List<Powerup> toBeRemoved;

    private void Update()
    {
        HandlePowerupExpiration();
    }

    public void HandlePowerupExpiration()
    {
        toBeRemoved = new List<Powerup>();

        foreach(Powerup pu in powerups)
        {
            pu.lifespan -= Time.deltaTime;

            if(pu.lifespan <= 0)
            {
                RemovePowerup(pu);
            }
        }

        foreach(Powerup pu in toBeRemoved)
        {
            powerups.Remove(pu);
        }
    }

    public void AddPowerup(Powerup powerupToAdd)
    {
        if (!powerupToAdd.isInfinite)
        {
            powerups.Add(powerupToAdd);
        }

        powerupToAdd.OnPickup();
    }

    public void RemovePowerup(Powerup powerupToRemove)
    {
        powerupToRemove.OnExpire();

        toBeRemoved.Add(powerupToRemove);
    }
}
