using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    Rigidbody rb;
    ShipData data;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //data = transform.parent.parent.gameObject.GetComponent<ShipData>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
