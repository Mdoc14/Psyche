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
        //Al entrar en el estado "BuscandoElSiguienteWaypoint" se selecciona como waypoint actual el siguiente del que ven�amos
        Transform[] waypoints = shadow.GetWaypoints();
        //Se realiza el m�dulo por la longitud del array del waypoints para volver al primero tras llegar al �ltimo:
        //waypoints.Length % waypoints.Length == 0
        int nextWaypointIndex = (Array.IndexOf(waypoints, shadow.GetCurrentWaypoint()) + 1) % waypoints.Length;

        shadow.SetCurrentWaypoint(waypoints[nextWaypointIndex]);
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        //Mientras el enemigo no est� mirando hacia su siguiente waypoint rotar�
        if (shadow.GetAngleTo(shadow.GetCurrentWaypoint()) != 0)
        {
            shadow.RotateTo(shadow.GetCurrentWaypoint());
        }
        else //Cuando mire hacia �l
        {
            shadow.SetState(new WalkingToAWaypoint(shadow)); //Cambios al estado de andar hacia el waypoint
        }
    }

    public override void Update()
    {
    }
}
