using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour
{
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float rotateSpeed;
    [Tooltip("Calculated at time of play. Tick 'Recalculate Shots Per Second' to update in realtime")]
    public float shotsPerSecond;
    [Tooltip("Tick after updating in playmode for realtime changes")]
    public bool recalculateShotsPerSecond;
    public float cannonballSpeed;
    public float cannonballDuration;
    public float damageDealt;
    public float crashDamage;
    public float maxHealth;
    public float scoreValue;

    public GameObject owner;
    public GameObject cannonballPrefab;
    public GameObject firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
