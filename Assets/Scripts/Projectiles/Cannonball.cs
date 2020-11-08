using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Projectiles
{

    // Start is called before the first frame update
    void Start()
    {
        damage = spawnOrigin.GetComponent<ShipData>().damageDealt;
    }

    //Destroys cannon on impact with anything
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
