using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChainshotPowerup : Powerup
{
    public TakeDamage health;

    // Start is called before the first frame update
    public override void OnPickup()
    {
        data.currentcannonPrefab = data.chainshotPrefab;
        shots++;
        base.OnPickup();
    }

    // Update is called once per frame
    public override void OnExpire()
    {
        data.currentcannonPrefab = data.cannonballPrefab;
        base.OnExpire();
    }
}
