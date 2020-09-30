using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : Mover
{
    private CharacterController cc;
    private ShipData data;
    public bool movingForward; //Used for calculating reverse speed

    // Start is called before the first frame update
    public override void Start()
    {
        cc = GetComponent<CharacterController>();
        data = GetComponent<ShipData>();
    }

    //Takes in a value from controllers to move towards
    public override void Move(Vector3 direction)
    {
        if (movingForward)
        {
            cc.SimpleMove(direction * data.forwardMoveSpeed * Time.deltaTime);
        }
        else
        {
            cc.SimpleMove(direction * data.backwardMoveSpeed * Time.deltaTime);
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
        cc.SimpleMove(transform.forward * data.forwardMoveSpeed);
    }

    public override void RotateTowards(Transform targetTransform)
    {
        //Rotate towards target
        Vector3 targetVector = targetTransform.position - transform.position;

        //Find target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);

        //Sets rotation
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotateSpeed);

        //Moves us towards new rotation
        transform.rotation = newRotation;
    }

}
