using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainshotProjectile : Projectiles
{
    // Start is called before the first frame update
    void Start()
    {
        damage = spawnOrigin.GetComponent<ShipData>().damageDealt / 2;
    }
}
