using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForAWaypoint : AShadowState
{
    public LookingForAWaypoint(IShadow shadow) : base(shadow)
    {
    }

    public override void Enter()
    {
        Transform[] waypoints = shadow.GetWaypoints();
        int nextWaypointIndex = (Array.IndexOf(waypoints, shadow.GetCurrentWaypoint()) + 1) % waypoints.Length;

        shadow.SetCurrentWaypoint(waypoints[nextWaypointIndex]);
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        if (shadow.GetAngleTo(shadow.GetCurrentWaypoint()) != 0)
        {
            shadow.RotateTo(shadow.GetCurrentWaypoint());
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
