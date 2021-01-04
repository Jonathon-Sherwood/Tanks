using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HumanController : Controller
{
    public ShipMover mover;
    public ShipShooter shooter;

    public Camera playerCamera;
    public Camera radarCamera;

    public enum ControlType {WASD, ArrowKeys, Controller1, Controller2, Dead};
    public ControlType controlType;

    public int lives;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GameManager.instance.humanPlayers.Add(this);
    }

    // Update is called once per frame
    void Update()
    { 

        if(mover == null) { return; }

        if (controlType == ControlType.WASD) //Set of controls based on using WASD keys
        {
            if (Input.GetKey(KeyCode.W))
            {
                //Move Forward (+)
                mover.Move(true);
            }

            if (Input.GetKey(KeyCode.S))
            {
                //Move Backward (-)
                mover.Move(false);
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Shoots Cannon
                if(shooter != null) shooter.Shoot();
            }
        }

        if (controlType == ControlType.ArrowKeys) //Set of controls based on using arrow keys
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //Move Forward (+)
                mover.Move(true);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                //Move Backward (-)
                mover.Move(false);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Rotate CounterClockwise (+)
                mover.Rotate(false);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                //Rotate Clockwise (-)
                mover.Rotate(true);
            }

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (shooter != null) shooter.Shoot();
            }
        }

        if (GameManager.instance.isPS4Controller)
        {
            if (controlType == ControlType.Controller1) //Set of controls based on being controller 1
            {
                if (Input.GetAxis("PS4Vert1") > 0.5)
                {
                    //Move forward (+)
                    mover.Move(true);
                }

                if (Input.GetAxis("PS4Vert1") < -0.5)
                {
                    //Move backward (-)
                }

                if (Input.GetAxis("PS4Hor1") > 0.5)
                {
                    //Rotate Clockwise (-)
                    mover.Rotate(true);
                }

                if (Input.GetAxis("PS4Hor1") < -0.5)
                {
                    //Rotate Counterclockwise (+)
                    mover.Rotate(false);
                }

                if (Input.GetButton("PS4Fire1"))
                {
                    //Shoots cannon
                    shooter.Shoot();
                }
            }

            if (controlType == ControlType.Controller2) //Set of controls based on being controller 2
            {
                if (Input.GetAxis("PS4Vert2") > 0.5)
                {
                    //Move forward (+)
                    mover.Move(true);
                }

                if (Input.GetAxis("PS4Vert2") < -0.5)
                {
                    //Move backward (-)
                }

                if (Input.GetAxis("PS4Hor2") > 0.5)
                {
                    //Rotate Clockwise (-)
                    mover.Rotate(true);
                }

                if (Input.GetAxis("PS4Hor2") < -0.5)
                {
                    //Rotate Counterclockwise (+)
                    mover.Rotate(false);
                }

                if (Input.GetButton("PS4Fire2"))
                {
                    //Shoots cannon
                    shooter.Shoot();
                }
            }
        }

        else 
        {
            if (controlType == ControlType.Controller1) //Set of controls based on being controller 1
            {
                if (Input.GetAxis("XBOXVert1") > 0.5)
                {
                    //Move forward (+)
                    mover.Move(true);
                }

                if (Input.GetAxis("XBOXVert1") < -0.5)
                {
                    //Move backward (-)
                }

                if (Input.GetAxis("XBOXHor1") > 0.5)
                {
                    //Rotate Clockwise (-)
                    mover.Rotate(true);
                }

                if (Input.GetAxis("XBOXHor1") < -0.5)
                {
                    //Rotate Counterclockwise (+)
                    mover.Rotate(false);
                }

                if (Input.GetAxis("XBOXFire1") > 0)
                {
                    //Shoots cannon
                    shooter.Shoot();
                }
            }

            if (controlType == ControlType.Controller2) //Set of controls based on being controller 2
            {
                if (Input.GetAxis("XBOXVert2") > 0.5)
                {
                    //Move forward (+)
                    mover.Move(true);
                }

                if (Input.GetAxis("XBOXVert2") < -0.5)
                {
                    //Move backward (-)
                }

                if (Input.GetAxis("XBOXHor2") > 0.5)
                {
                    //Rotate Clockwise (-)
                    mover.Rotate(true);
                }

                if (Input.GetAxis("XBOXHor2") < -0.5)
                {
                    //Rotate Counterclockwise (+)
                    mover.Rotate(false);
                }

                if (Input.GetAxis("XBOXFire2") > 0)
                {
                    //Shoots cannon
                    shooter.Shoot();
                }
            }
        }
    }
}
