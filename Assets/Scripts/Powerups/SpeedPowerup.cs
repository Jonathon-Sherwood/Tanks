using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeedPowerup : Powerup
{

    public override void OnPickup()
    {
        //Adds adjustable speed to ship movement
        data.forwardMoveSpeed += amount;
        base.OnPickup();
    }


    public override void OnExpire()
    {
        //removes adjustable speed to ship movement
        data.forwardMoveSpeed -= amount;
        base.OnExpire();
    }
}
