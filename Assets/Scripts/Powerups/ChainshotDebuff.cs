using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChainshotDebuff : Powerup
{

    public override void OnPickup()
    {
        //Halts movement
        data.forwardMoveSpeed = amount;
        base.OnPickup();
    }


    public override void OnExpire()
    {
        //Returns movement to normal
        data.forwardMoveSpeed = data.maxSpeed;
        base.OnExpire();
    }
}
