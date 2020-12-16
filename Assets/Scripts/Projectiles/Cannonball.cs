using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Projectiles
{
    AudioSource audioSource;
    public AudioClip fire;
    public AudioClip impact;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        damage = spawnOrigin.GetComponent<ShipData>().damageDealt;
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, GameManager.instance.sfxAudio);
    }

    //Destroys cannon on impact with anything
    private void OnTriggerEnter(Collider other)
    {
        audioSource.clip = impact;
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, GameManager.instance.sfxAudio);
        if(other.CompareTag("Ship"))
        {
            Instantiate(shrapnelPrefab, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
