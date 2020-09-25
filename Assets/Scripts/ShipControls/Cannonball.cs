using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject spawnOrigin; //Given a value by the ship that instantiates this
    float damage; //Pulls from the ship that instantiates this

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
