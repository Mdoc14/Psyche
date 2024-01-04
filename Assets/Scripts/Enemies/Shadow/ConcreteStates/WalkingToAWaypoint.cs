using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingToAWaypoint : AShadowState
{
    float lastTimeSearching = 0.0f;

    public WalkingToAWaypoint(IShadow shadow) : base(shadow)
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
        if (shadow.GetDistanceTo(shadow.GetCurrentWaypoint()) > 0.5f)
        {
            shadow.MoveTo(shadow.GetCurrentWaypoint(), shadow.GetBasicMovementSpeed());
        }
        else
        {
            shadow.SetState(new LookingForAWaypoint(shadow));
        }
    }

    public override void Update()
    {
        lastTimeSearching += Time.deltaTime;

        if (lastTimeSearching >= shadow.GetBasicMovementSpeed())
        {
            shadow.SetState(new LookingForPlayer(shadow));
        }
    }
}
