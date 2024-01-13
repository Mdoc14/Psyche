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
        //Al entrar en el estado "BuscandoElSiguienteWaypoint" se selecciona como waypoint actual el siguiente del que veníamos
        Transform[] waypoints = shadow.GetWaypoints();
        //Se realiza el módulo por la longitud del array del waypoints para volver al primero tras llegar al último:
        //waypoints.Length % waypoints.Length == 0
        int nextWaypointIndex = (Array.IndexOf(waypoints, shadow.GetCurrentWaypoint()) + 1) % waypoints.Length;

        shadow.SetCurrentWaypoint(waypoints[nextWaypointIndex]);
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        //Mientras el enemigo no esté mirando hacia su siguiente waypoint rotará
        if (shadow.GetAngleTo(shadow.GetCurrentWaypoint()) != 0)
        {
            shadow.RotateTo(shadow.GetCurrentWaypoint());
        }
        else //Cuando mire hacia él
        {
            shadow.SetState(new WalkingToAWaypoint(shadow)); //Cambios al estado de andar hacia el waypoint
        }
    }

    public override void Update()
    {
    }
}
