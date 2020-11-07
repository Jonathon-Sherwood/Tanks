using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainshot : Projectiles
{

    public float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        damage = spawnOrigin.GetComponent<ShipData>().damageDealt / 2;
    }

    private void Update()
    {
        Spin();
    }

    void Spin()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }

    //Destroys cannon on impact with anything
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Cannon"))
        {
            Destroy(this.gameObject);
        }
    }
}
