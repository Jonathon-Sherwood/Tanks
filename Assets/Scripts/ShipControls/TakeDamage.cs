using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private float currentHealth;
    ShipData data;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<ShipData>();
        currentHealth = data.maxHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    //Destroys this gameobject once health reaches 0
    private void Die()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Input amount of damage taken from outside sources
    /// </summary>
    /// <param name="damage"></param>
    public void DamageTaken(float damage)
    {
        currentHealth -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannon")) //Accepts damage from sources on hit from cannon
        {
            GameObject attacker = other.gameObject.GetComponent<Cannonball>().spawnOrigin;
            if(attacker == null) { return; }
            float damageDealt = attacker.GetComponent<ShipData>().damageDealt;
            DamageTaken(damageDealt);

            if(currentHealth <= 0) //Gives score to killer on death based on shipdata value
            {
                attacker.GetComponent<ShipData>().owner.GetComponent<ScoreTracker>().currentScore += data.scoreValue;
            }
        }
    }

    //Will be implemented when rigidbody replaces character controller
    /*private void OnCollisionEnter(Collision collision)
    {
        if (!collision.rigidbody.CompareTag("Floor") && !collision.rigidbody.CompareTag("Ship"))
        {
            DamageTaken(data.crashDamage);
            print(collision.gameObject.name);
        }
    }*/
}
