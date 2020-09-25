using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    private ShipData data;
    private float countdown; //Used for rounds per minute
    private float shotsPerSecond;

    private void Start()
    {
        data = GetComponent<ShipData>();
        shotsPerSecond = 60 / data.shotsPerSecond; //Becomes a time in comparison to a minute
        shotsPerSecond = shotsPerSecond / 60; //Turns the minute into shots per second
    }

    private void Update()
    {
        //Counts down timer stopping at 0
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            countdown = 0;
        }
        Recalculate();
    }

    //Fires cannon forward and refreshes cooldown
    public void Shoot()
    {
        if (countdown <= 0)
        {
            countdown += shotsPerSecond + Time.deltaTime;
            GameObject cannon = Instantiate(data.cannonballPrefab, data.firePoint.transform.position, Quaternion.identity) as GameObject;
            Rigidbody cannonRB = cannon.GetComponent<Rigidbody>();
            cannon.GetComponent<Cannonball>().spawnOrigin = gameObject;
            cannonRB.velocity = transform.TransformDirection(Vector3.forward * data.cannonballSpeed * Time.deltaTime);
            Destroy(cannon, data.cannonballDuration);
        }
    }

    //Used in the inspector to update the shots per second calculation during playtime
    void Recalculate()
    {
        if (data.recalculateShotsPerSecond)
        {
            shotsPerSecond = 60 / data.shotsPerSecond; //Becomes a time in comparison to a minute
            shotsPerSecond = shotsPerSecond / 60; //Turns the minute into shots per second
            data.recalculateShotsPerSecond = false;
        }
    }
}
