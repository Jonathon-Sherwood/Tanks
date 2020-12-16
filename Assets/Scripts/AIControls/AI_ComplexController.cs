using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ComplexController : AIController
{
    private void Awake()
    {

    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GameManager.instance.aiPlayers.Add(this);
        originalSpeed = data.forwardMoveSpeed;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (data == null) //Removes from list and prevents crashing
        {
            GameManager.instance.aiPlayers.Remove(this);
            return;
        }

        if (GameManager.instance.playerShipData == null && GameManager.instance.player2ShipData == null) //Prevents looking for player if there is none
        {
            return;
        }
        switch (currentState)
        {
            case AIStates.Patrol:
                Patrol();

                //Check for state change
                if (CanSee(target))
                {
                    ChangeState(AIStates.AttackTarget);

                }

                if (CanHear(target))
                {
                    ChangeState(AIStates.Spin);
                    lastStateChangeTime = Time.time;
                }
                break;


            case AIStates.Spin:
                Rotate();
                //Check for state change
                if (CanSee(target))
                {
                    ChangeState(AIStates.AttackTarget);
                }

                //Returns to patrol after not finding the player
                if(Time.time > lastStateChangeTime + searchTime)
                {
                    ChangeState(AIStates.Patrol);
                }
                break;
            case AIStates.AttackTarget:
                AttackTarget();
                StoppingDistance();

                //If I cannot see the player...
                if(CanSee(target))
                {
                    //...Start the countdown
                    lastStateChangeTime = Time.time;
                }
                //If the countdown is up...
                if (Time.time > lastStateChangeTime + chaseTime)
                {
                    //...Return to spin
                    ChangeState(AIStates.Spin);
                    lastStateChangeTime = Time.time;
                }

                break;
            default:
                Debug.LogWarning("STATE NOT FOUND");
                currentState = AIStates.Idle;
                break;
        }
    }
}
