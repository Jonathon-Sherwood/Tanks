using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public ShipMover mover;
    private ShipShooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        shooter = mover.gameObject.GetComponent<ShipShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        shooter.Shoot();
    }
}
