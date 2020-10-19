using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RamController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        GameManager.instance.aiPlayers.Add(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (data == null)
        {
            //Prevents crashes and removes enemy from gamemanager list
            GameManager.instance.aiPlayers.Remove(this);
            return;
        }
        if (GameManager.instance.playerShipData == null) //Prevents looking for player if there is none
        {
            return;
        }


        switch (currentState)
        {
            case AIStates.AttackTarget:
                TargetPlayer();
                AttackTarget();

                //Check for state change
                if (PlayerCanSee(target))
                {
                    ChangeState(AIStates.Flee);
                }
                break;

            case AIStates.Flee:
                TargetPlayer();
                Flee(target.transform);
                //Check for state change
                if (!PlayerCanSee(target))
                {
                    ChangeState(AIStates.AttackTarget);
                }
                break;

        }
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
