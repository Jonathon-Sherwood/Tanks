using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public ShipShooter shooter;
    public ShipMover mover;
    private ShipData data;

    //Locks shots to a range from smallest to cannon cooldown
    [Range(.1f,3)]
    [Tooltip("Cannot shoot faster than shots per second on all ships. Must fire once before new value accepted. This is why it cannot be 0. Call CanShoot bool to cancel outright.")]
    public float fireFrequency;
    private float fireCountdown;
    public bool canShoot = true; //Used in inspector to cancel shots outright

    public List<Waypoint> waypoints; //Fillable list of locations for the patrol to move to
    private int currentWaypointIndex = 0;
    public float waypointBufferDistance = 1f;

    public enum PatrolType { Stop, Loop, PingPong, Random}
    public PatrolType patrolType;
    [HideInInspector] public bool isPatrolling = true;
    [HideInInspector] public bool isPatrolForward = true;

    private void Start()
    {
        data = mover.GetComponent<ShipData>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            Patrol();
        }
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

    public void Patrol()
    {

        //Turn towards waypoint and move forward
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(data.transform.position, waypointBufferDistance);
    }

}
