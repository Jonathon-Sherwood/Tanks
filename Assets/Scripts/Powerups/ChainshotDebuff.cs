using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChainshotDebuff : Powerup
{
    // Start is called before the first frame update
    public override void OnPickup()
    {
        data.forwardMoveSpeed = amount;
        base.OnPickup();
    }

    // Update is called once per frame
    public override void OnExpire()
    {
        data.forwardMoveSpeed = data.maxSpeed;
        base.OnExpire();
    }
}
