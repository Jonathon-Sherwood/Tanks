using System.Collections;
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
    public GameObject[] waypoints; 
    private int currentWaypointIndex = 0;
    public float waypointBufferDistance = 1f;
    public float obstacleAvoidanceDistance = 5;
    public float avoidanceTime = 2;
    [HideInInspector] public float exitTime;
    public enum PatrolType { Stop, Loop, PingPong, Random}
    public PatrolType patrolType;
    [HideInInspector] public bool isPatrolling = true;
    [HideInInspector] public bool isPatrolForward = true;
    private int pauseTime = 10;
    private float pauseCountdown;
    private bool pauseOver = false;
    public float stopDistance;
    [HideInInspector] public float originalSpeed;
    bool stopped;

    //Statemachine
    public enum AIStates { Idle, Spin, AttackTarget, Flee, Patrol}
    public AIStates currentState = AIStates.Idle;
    [HideInInspector] public float lastStateChangeTime;
    public float searchTime = 5f;
    public float chaseTime = 10f;


    //Targetting and Senses
    public GameObject target;
    public float fieldOfView = 60f;
    public float viewDistance = 10f;
    public float hearingSensitivity = 1f;


    public virtual void Start()
    {
        pauseCountdown = pauseTime;
    }

    public virtual void Update()
    {
        if(GameManager.instance.playerShipData == null)
        {
            return;
        }
        pauseCountdown--;
        if (pauseCountdown < 0)
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            pauseOver = true;
        }
        if(target == null)
        {
            if(GameManager.instance.playerShipData != null)
            {
                target = GameManager.instance.playerShipData.gameObject;
            }
            else
            {
                target = GameManager.instance.player2ShipData.gameObject;
            }
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
        //target = GameManager.instance.humanPlayers[0].data.gameObject;
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
        if(target == null) { return false; }
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
        if(pauseOver != true) { return; }  //Waits until waypoints have been populated to look for them

        //Turn towards waypoint and move forward
        data.mover.MoveTo(waypoints[currentWaypointIndex].transform);

        //If we are "close enough" to the waypoint, advance to the next waypoint
        if (Vector3.Distance(data.transform.position, waypoints[currentWaypointIndex].transform.position) < waypointBufferDistance)
        {
            if(patrolType == PatrolType.Random)
            {
                currentWaypointIndex = Random.Range(0, waypoints.Length);
            }

            if (isPatrolForward && patrolType != PatrolType.Random)
            {
                currentWaypointIndex++;
            }
            else if (!isPatrolForward && patrolType != PatrolType.Random)
            {
                currentWaypointIndex--;
            }
        }

        //Loop end
        if (currentWaypointIndex >= waypoints.Length)
        {
            if (patrolType == PatrolType.Loop) //Full Circle
            {
                currentWaypointIndex = 0;
            }
            else if (patrolType == PatrolType.Stop) //Stop patrolling
            {
                isPatrolling = false;
            }
            else if (patrolType == PatrolType.PingPong) //Start moving in the opposite direction
            {
                isPatrolForward = !isPatrolForward;

                //Keep waypoints within range
                currentWaypointIndex = Mathf.Clamp(currentWaypointIndex, 1, waypoints.Length - 1);
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

    public void StoppingDistance()
    {
        //As the ship reaches a certain distance from its target...
        if ((Vector3.Distance(target.transform.position, data.transform.position) < stopDistance))
        {
            //...Stop moving
            data.forwardMoveSpeed = 0;
            stopped = true;
        }
        //The moment the target leaves range...
        else if ((Vector3.Distance(target.transform.position, data.transform.position) > stopDistance) && stopped)
        {
            //...Return to original speed
            data.forwardMoveSpeed = originalSpeed;
            stopped = false;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.aiPlayers.Remove(this);
    }
}
