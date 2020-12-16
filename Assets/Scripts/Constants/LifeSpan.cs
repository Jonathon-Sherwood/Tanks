using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    public float lifetime;

    void Start()
    {
        lifetime = Time.time + lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= Time.time)
        {
            Destroy(this.gameObject);
        }
    }
}
