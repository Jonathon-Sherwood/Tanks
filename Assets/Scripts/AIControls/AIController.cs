using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public ShipMover mover;
    private ShipShooter shooter;
    private ShipData data;

    //Locks shots to a range from smallest to cannon cooldown
    [Range(.1f,3)]
    [Tooltip("Cannot shoot faster than shots per second on all ships. Must fire once before new value accepted. This is why it cannot be 0. Call CanShoot bool to cancel outright.")]
    public float fireFrequency; 

    private float fireCountdown;
    public bool canShoot = true; //Used in inspector to cancel shots outright

    // Start is called before the first frame update
    void Start()
    {
        shooter = mover.gameObject.GetComponent<ShipShooter>();
        data = mover.gameObject.GetComponent<ShipData>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shoots at an adjustable rate, no faster than any cannon cooldown
        if (Time.time >= fireCountdown && canShoot)
        {
            shooter.Shoot();
            fireCountdown = Time.time + 1f / fireFrequency;
        }
    }
}
