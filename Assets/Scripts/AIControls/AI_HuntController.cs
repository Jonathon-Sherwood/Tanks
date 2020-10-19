﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HuntController : AIController
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
        TargetPlayer();
        AttackTarget();
    }
}
