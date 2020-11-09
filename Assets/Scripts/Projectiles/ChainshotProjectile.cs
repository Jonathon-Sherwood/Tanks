using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainshotProjectile : Projectiles
{
    // Start is called before the first frame update
    void Start()
    {
        //Sets chainshot to be half the strength of a regular cannon
        damage = spawnOrigin.GetComponent<ShipData>().damageDealt / 2;
    }
}
