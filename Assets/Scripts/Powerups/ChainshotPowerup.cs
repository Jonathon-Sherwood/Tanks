using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChainshotPowerup : Powerup
{
    public TakeDamage health;


    public override void OnPickup()
    {
        //Changes ships cannons from cannonballs to chainshot debuff shots
        data.currentcannonPrefab = data.chainshotPrefab;
        base.OnPickup();
    }

    public override void OnExpire()
    {
        //Reverts ships cannons back to cannonballs
        data.currentcannonPrefab = data.cannonballPrefab;
        base.OnExpire();
    }
}
