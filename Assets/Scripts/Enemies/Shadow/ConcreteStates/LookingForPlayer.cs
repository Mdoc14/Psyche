using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForPlayer : AShadowState
{
    public LookingForPlayer(IShadow shadow) : base(shadow)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        if(shadow.GetPlayerAtSight() != null)
        {
            shadow.SetState(new AttackingPlayer(shadow));
        }
        else
        {
            shadow.SetState(new WalkingToAWaypoint(shadow));
        }
    }

    public override void Update()
    {
    }
}
