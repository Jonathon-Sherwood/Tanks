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

    private void Die()
    {
        Destroy(this.gameObject);
    }

    public void DamageTaken(float damage)
    {
        currentHealth -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannon"))
        {
            GameObject attacker = other.gameObject.GetComponent<Cannonball>().spawnOrigin;
            float damageDealt = attacker.GetComponent<ShipData>().damageDealt;
            DamageTaken(damageDealt);

            if(currentHealth <= 0)
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
