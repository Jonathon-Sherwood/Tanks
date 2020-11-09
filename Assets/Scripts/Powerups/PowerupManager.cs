using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager :  MonoBehaviour
{
    public List<Powerup> powerups; //Contains a list of all powerups added
    private List<Powerup> toBeRemoved; //Used to hold onto expired powerups

    private void Update()
    {
        HandlePowerupExpiration();
    }

    public void HandlePowerupExpiration()
    {
        //Empties toberemoved list and creates it
        toBeRemoved = new List<Powerup>();

        foreach(Powerup pu in powerups) //Counts down each powerups timer until expiration
        {
            pu.lifespan -= Time.deltaTime;

            if(pu.lifespan <= 0) //Adds all expired powerups to list of removals
            {
                RemovePowerup(pu);
            }
        }

        foreach(Powerup pu in toBeRemoved) //Removes all expired powerups after going through list
        {
            powerups.Remove(pu);
        }
    }

    public void AddPowerup(Powerup powerupToAdd) //Callable function to add to powerup list
    {
        if (!powerupToAdd.isInfinite)
        {
            powerups.Add(powerupToAdd);
        }

        powerupToAdd.OnPickup();
    }

    public void RemovePowerup(Powerup powerupToRemove) //Callable function to remove to powerup list
    {
        powerupToRemove.OnExpire();

        toBeRemoved.Add(powerupToRemove);
    }
}
