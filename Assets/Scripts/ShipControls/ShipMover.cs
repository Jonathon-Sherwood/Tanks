using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : Mover
{
    private ShipData data;
    private Rigidbody rb;
    private AIController aiController;
    public bool movingForward; //Used for calculating reverse speed

    //Obstacle Avoidance for AI
    public enum AIAvoidanceState { Normal, TurnToAvoid, MoveToAvoid }
    public AIAvoidanceState currentAvoidState = AIAvoidanceState.Normal;
    public float lastAvoidanceStateChangeTime;

    // Start is called before the first frame update
    public override void Start()
    {
        data = GetComponent<ShipData>();
        rb = GetComponent<Rigidbody>();
        aiController = data.owner.GetComponent<AIController>();
    }

    public override void Update()
    {

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

    public bool CanMoveForward(float speed)
    {
        // Cast a ray forward in the distance that we sent in
        // If our raycast hit something
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, speed))
        {
            return false;
        }
        // otherwise, return true
        return true;
    }


    public override void MoveTo(Transform targetTransform)
    {
        if (currentAvoidState == AIAvoidanceState.Normal)
        {
            Vector3 movement = transform.rotation * Vector3.forward;

            //Rotate towards the transform
            RotateTowards(targetTransform);

            if (CanMoveForward(aiController.obstacleAvoidanceDistance))
            {
                //Move forward
                rb.velocity = movement * data.forwardMoveSpeed * Time.deltaTime;
            }
            else
            {
                currentAvoidState = AIAvoidanceState.TurnToAvoid;
            }
        } else
        {
            Avoidance();
        }
    }

    public override void RotateTowards(Transform targetTransform)
    {
        Vector3 targetPosition = targetTransform.position;
        targetPosition.y = transform.position.y;

        //Rotate towards target
        Vector3 targetVector = targetPosition - transform.position;

        //Find target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);

        //Sets rotation
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotateSpeed * Time.deltaTime);

        //Moves us towards new rotation
        transform.rotation = newRotation;
    }

    public override void MoveAway(Transform targetTransform)
    {
        if (currentAvoidState == AIAvoidanceState.Normal)
        {
            Vector3 movement = transform.rotation * Vector3.forward;

            //Rotate towards the opposite transform
            RotateAway(targetTransform);

            if (CanMoveForward(aiController.obstacleAvoidanceDistance))
            {
                //Move forward
                rb.velocity = movement * data.forwardMoveSpeed * Time.deltaTime;
            }
            else
            {
                currentAvoidState = AIAvoidanceState.TurnToAvoid;
            }
        }
        else
        {
            Avoidance();
        }
    }

    public override void RotateAway(Transform targetTransform)
    {
        Vector3 targetPosition = targetTransform.position;
        targetPosition.y = transform.position.y;

        //Rotate away from target
        Vector3 targetVector = targetPosition + transform.position;

        //Find opposite rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);

        //Sets rotation
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotateSpeed * Time.deltaTime);

        //Moves us towards new rotation
        transform.rotation = newRotation;
    }

    // DoAvoidance - handles obstacle avoidance
    void Avoidance()
    {
        if (currentAvoidState == AIAvoidanceState.TurnToAvoid)
        {
            // Rotate left
            Rotate(true);

            // If I can now move forward, move to stage 2!
            if (CanMoveForward(aiController.obstacleAvoidanceDistance))
            {
                currentAvoidState = AIAvoidanceState.MoveToAvoid;

                // Set the number of seconds we will stay in Stage2
                aiController.exitTime = aiController.avoidanceTime;
            }

            // Otherwise, we'll do this again next turn!
        }
        else if (currentAvoidState == AIAvoidanceState.MoveToAvoid)
        {
            // if we can move forward, do so
            if (CanMoveForward(aiController.obstacleAvoidanceDistance))
            {
                // Subtract from our timer and move
                aiController.exitTime -= Time.deltaTime;
                Move(true);

                // If we have moved long enough, return to chase mode
                if (aiController.exitTime <= 0)
                {
                    currentAvoidState = AIAvoidanceState.Normal;
                }
            }
            else
            {
                // Otherwise, we can't move forward, so back to stage 1
                currentAvoidState = AIAvoidanceState.TurnToAvoid;
            }
        }
    }
}
