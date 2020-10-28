using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Master class that holds all data regarding stats for ships
/// </summary>
public class ShipData : MonoBehaviour
{
    public float forwardMoveSpeed;
    public float maxSpeed;
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
    public float noiseMaker;
    [HideInInspector] public Mover mover;
    [HideInInspector] public TakeDamage health;

    [Tooltip("Place controlling player here")] public GameObject owner;
    [Tooltip("Place cannonball from prefabs folder here")] public GameObject cannonballPrefab;
    [Tooltip("Place firepoint gameobject that cannons will fire from here")] public GameObject firePoint;

    private void Start()
    {
        mover = GetComponent<ShipMover>();
        health = gameObject.GetComponent<TakeDamage>();
        GameManager.instance.shipList.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.shipList.Remove(this);
    }
}
