using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : MonoBehaviour
{
    private CharacterController cc;
    private ShipData data;
    public bool movingForward;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        data = GetComponent<ShipData>();
    }

    public void Move(Vector3 direction)
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

    public void Rotate(bool isClockwise)
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

}
