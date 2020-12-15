using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RamController : AIController
{
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
        if (data == null)
        {
            //Prevents crashes and removes enemy from gamemanager list
            GameManager.instance.aiPlayers.Remove(this);
            return;
        }
        if (GameManager.instance.playerShipData == null && GameManager.instance.player2ShipData == null) //Prevents looking for player if there is none
        {
            return;
        }


        switch (currentState)
        {
            case AIStates.AttackTarget:
                TargetPlayer();
                AttackTarget();
                StoppingDistance();

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
}
