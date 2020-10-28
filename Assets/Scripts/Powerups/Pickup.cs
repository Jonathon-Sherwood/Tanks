using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Spin();
    }

    public void Spin()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
