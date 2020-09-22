using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public ShipMover mover;

    public enum ControlType {WASD, ArrowKeys, GamePad};
    public ControlType controlType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToMove = Vector3.zero;

        if(controlType == ControlType.WASD)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //Move Forward (+)
                directionToMove = mover.transform.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                //Move Backward (-)
                directionToMove = -mover.transform.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                //Rotate CounterClockwise (+)
                mover.Rotate(false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                //Rotate Clockwise (-)
                mover.Rotate(true);
            }
        }

        if (controlType == ControlType.ArrowKeys)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //Move Forward (+)
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                //Move Backward (-)
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Rotate CounterClockwise (+)
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                //Rotate Clockwise (-)
            }
        }

        if (controlType == ControlType.GamePad)
        {

        }

        mover.Move(directionToMove);
    }
}
