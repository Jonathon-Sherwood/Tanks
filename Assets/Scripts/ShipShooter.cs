using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    private ShipData data;

    private void Start()
    {
        data = GetComponent<ShipData>();
    }

    public void Shoot()
    {
        GameObject cannon = Instantiate(data.cannonballPrefab, data.firePoint.transform.position, Quaternion.identity) as GameObject;
        Rigidbody cannonRB = cannon.GetComponent<Rigidbody>();
        cannonRB.velocity = transform.TransformDirection(Vector3.forward * data.cannonballSpeed * Time.deltaTime);
        Destroy(cannon, data.cannonballDuration);
    }
}
