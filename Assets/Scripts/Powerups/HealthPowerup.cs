using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public TakeDamage health;

    // Start is called before the first frame update
    public override void OnPickup()
    {
        data.health.currentHealth += amount;
        base.OnPickup();
    }

    // Update is called once per frame
    public override void OnExpire()
    {
        data.health.currentHealth -= amount;
        base.OnExpire();
    }
}
