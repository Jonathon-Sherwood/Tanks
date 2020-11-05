﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    //Connections to Ship
    public ShipShooter shooter;
    public ShipMover mover;

    //Locks shots to a range from smallest to cannon cooldown
    [Range(.1f,3)]
    [Tooltip("Cannot shoot faster than shots per second on all ships. Must fire once before new value accepted. This is why it cannot be 0. Call CanShoot bool to cancel outright.")]
    public float fireFrequency;
    private float fireCountdown;
    public bool canShoot = true; //Used in inspector to cancel shots outright

    //Waypoints and Movement
    public List<Waypoint> waypoints; 
    private int currentWaypointIndex = 0;
    public float waypointBufferDistance = 1f;
    public float obstacleAvoidanceDistance = 5;
    public float avoidanceTime = 2;
    [HideInInspector] public float exitTime;
    public enum PatrolType { Stop, Loop, PingPong, Random}
    public PatrolType patrolType;
    [HideInInspector] public bool isPatrolling = true;
    [HideInInspector] public bool isPatrolForward = true;

    //Statemachine
    public enum AIStates { Idle, Spin, AttackTarget, Flee, Patrol}
    public AIStates currentState = AIStates.Idle;
    public float lastStateChangeTime;


    //Targetting and Senses
    public GameObject target;
    public float fieldOfView = 60f;
    public float viewDistance = 10f;
    public float hearingSensitivity = 1f;

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if(GameManager.instance.playerShipData == null)
        {
            return;
        }
    }

    public void ChangeState(AIStates newState)
    {
        //Set the state
        currentState = newState;
        //save the time
        lastStateChangeTime = Time.time;
    }


    public void TargetPlayer()
    {

        target = GameManager.instance.humanPlayers[0].data.gameObject;
    }

    //Stops AI from doing anything
    public void Idle() { }

    public void Rotate()
    {
        //Spins indefinitely
        data.mover.Rotate(true);
    }


    public void Shoot()
    {
        //Shoots at an adjustable rate, no faster than any cannon cooldown
        if (Time.time >= fireCountdown && canShoot)
        {
            shooter.Shoot();
            fireCountdown = Time.time + 1f / fireFrequency;
        }
    }

    public void AttackTarget()
    {
        if(target != null)
        {
            //Move to target
            data.mover.MoveTo(target.transform);

            if (canShoot)
            {
                //shoot
                Shoot();
            }

        }
    }


    public bool CanSee(GameObject target)
    {
        //Field of View Check
        if(target == null) { return false; }
        Vector3 vectorToTarget = target.transform.position - data.transform.position;
        float angle = Vector3.Angle(data.transform.forward, vectorToTarget);
        if(angle > fieldOfView)
        {
            return false;
        }

        //Line of Sight Check
        RaycastHit hitInfo;
        if (Physics.Raycast(data.transform.position, data.transform.forward, out hitInfo, viewDistance))
        {
            if (hitInfo.collider.gameObject != target)
            {
                return false;
            }
            else
            {
                //If made it through checks can see target
                return true;
            }
        }
        return false;
    }

    public bool CanHear(GameObject target)
    {
        //Can hear
        if(Vector3.Distance(target.transform.position, data.transform.position) < hearingSensitivity * target.GetComponent<ShipData>().noiseMaker)
        {
            return true;
        }

        //Cannot hear
        return false;
    }

    public void Patrol()
    {
            //Turn towards waypoint and move forward
            //if(data == null) { return; }
            data.mover.MoveTo(waypoints[currentWaypointIndex].transform);

            //If we are "close enough" to the waypoint, advance to the next waypoint
            if (Vector3.Distance(data.transform.position, waypoints[currentWaypointIndex].transform.position) < waypointBufferDistance)
            {
                if (isPatrolForward)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    currentWaypointIndex--;
                }
            }

            //Loop end
            if (currentWaypointIndex >= waypoints.Count)
            {
                if (patrolType == PatrolType.Loop) //Full Circle
                {
                    currentWaypointIndex = 0;
                }
                else if (patrolType == PatrolType.Random) //Random waypoints
                {
                    currentWaypointIndex = Random.Range(0, waypoints.Count);
                }
                else if (patrolType == PatrolType.Stop) //Stop patrolling
                {
                    isPatrolling = false;
                }
                else if (patrolType == PatrolType.PingPong) //Start moving in the opposite direction
                {
                    isPatrolForward = !isPatrolForward;

                    //Keep waypoints within range
                    currentWaypointIndex = Mathf.Clamp(currentWaypointIndex, 1, waypoints.Count - 1);
                }
            }
    }

    public void Flee(Transform target)
    {

        mover.MoveAway(target);
    }

    public bool PlayerCanSee(GameObject target)
    {
        //Stops breaking on null
        if (target == null) { return false; }

        //Starts the vector from the player ship
        Vector3 vectorToTarget = data.transform.position - target.transform.position;
        //Sets angle between player ship and this ship
        float angle = Vector3.Angle(target.transform.forward, vectorToTarget);
        //If player's angle is facing this ship and is nearby, flee
        if (angle < fieldOfView && (Vector3.Distance(target.transform.position, data.transform.position) < viewDistance))
        {
            ChangeState(AIStates.Flee);
            return true;
        }

        return false;
    }
}
