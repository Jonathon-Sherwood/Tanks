using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SimpleController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        switch (currentState)
        {
            case AIStates.Idle:
                TargetPlayer();
                Idle();

                //Check for state change
                if (CanSee(target))
                {
                    currentState = AIStates.AttackTarget;
                }

                if (CanHear(target))
                {
                    currentState = AIStates.Spin;
                }
                break;


            case AIStates.Spin:
                Rotate();
                TargetPlayer();

                //Check for state change
                if (CanSee(target))
                {
                    currentState = AIStates.AttackTarget;
                }
                break;
            case AIStates.AttackTarget:
                AttackPlayer();

                //Check for state change
                if (!CanSee(target))
                {
                    currentState = AIStates.Spin;
                }
                break;
            default:
                print("STATE NOT FOUND");
                currentState = AIStates.Idle;
                break;
        }
    }
}
