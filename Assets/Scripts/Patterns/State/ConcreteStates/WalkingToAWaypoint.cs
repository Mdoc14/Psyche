using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingToAWaypoint : AShadowState
{
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
        //Si estamos lejos del waypoint nos movemos hacia él
        if (shadow.GetDistanceTo(shadow.GetCurrentWaypoint()) > 0.5f)
        {
            shadow.MoveTo(shadow.GetCurrentWaypoint(), shadow.GetBasicMovementSpeed());
        }
        else //Cuando estemos cerca
        {
            shadow.SetState(new LookingForAWaypoint(shadow)); //Pasamos a buscar el siguiente waypoint
        }
    }

    public override void Update()
    {
    }
}
