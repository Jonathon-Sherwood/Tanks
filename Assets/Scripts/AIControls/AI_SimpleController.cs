﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SimpleController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        GameManager.instance.aiPlayers.Add(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (data == null) //Removes from list and prevents crashing
        {
            GameManager.instance.aiPlayers.Remove(this);
            return;
        }

        if (GameManager.instance.playerShipData == null) //Prevents looking for player if there is none
        {
            return;
        }
        switch (currentState)
        {
            case AIStates.Idle:
                TargetPlayer();
                Idle();

                //Check for state change
                if (CanSee(target))
                {
                    ChangeState(AIStates.AttackTarget);

                }

                if (CanHear(target))
                {
                    ChangeState(AIStates.Spin);
                }
                break;


            case AIStates.Spin:
                Rotate();
                TargetPlayer();

                //Check for state change
                if (CanSee(target))
                {
                    ChangeState(AIStates.AttackTarget);
                }
                break;
            case AIStates.AttackTarget:
                AttackPlayer();

                //Check for state change
                if (!CanSee(target))
                {
                    ChangeState(AIStates.Spin);
                }
                break;
            default:
                print("STATE NOT FOUND");
                currentState = AIStates.Idle;
                break;
        }
    }
}
