using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    Rigidbody rb;

    public GameObject spawnOrigin;
    float damage;

    // Start is called before the first frame update
    void start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        damage = spawnOrigin.GetComponent<ShipData>().damageDealt;
        print(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Ship"))
        //{
        //    other.gameObject.GetComponent<TakeDamage>().currentHealth -= spawnOrigin.GetComponent<ShipData>().damageDealt;
        //    Destroy(this.gameObject);
        //}
        //else
        //{
            Destroy(this.gameObject);
        //}
    }
}
