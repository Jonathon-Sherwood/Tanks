using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : Mover
{
    private ShipData data;
    private Rigidbody rb;
    public bool movingForward; //Used for calculating reverse speed

    // Start is called before the first frame update
    public override void Start()
    {
        data = GetComponent<ShipData>();
        rb = GetComponent<Rigidbody>();
    }


    //Takes in a value from controllers to move towards
    public override void Move(bool movingForward)
    {
        Vector3 movement = transform.rotation * Vector3.forward;

        if (movingForward)
        {
            rb.velocity = movement * data.forwardMoveSpeed * Time.deltaTime;
        }
        else if (!movingForward)
        {
            //rb.velocity = -movement * data.backwardMoveSpeed * Time.deltaTime;
        }
    }

    //Recieves bool from controllers for rotation
    public override void Rotate(bool isClockwise)
    {
        if (isClockwise)
        {
            transform.Rotate(new Vector3(0, data.rotateSpeed * Time.deltaTime, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, -data.rotateSpeed * Time.deltaTime, 0));
        }
    }

    public override void MoveTo(Transform targetTransform)
    {
        //Rotate towards the transform
        RotateTowards(targetTransform);

        //Move forward
        transform.position += transform.forward * data.forwardMoveSpeed * Time.deltaTime;
    }

    public override void RotateTowards(Transform targetTransform)
    {
        //Rotate towards target
        Vector3 targetVector = targetTransform.position - transform.position;

        //Find target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);

        //Sets rotation
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotateSpeed * Time.deltaTime);

        //Moves us towards new rotation
        transform.rotation = newRotation;
    }

}
