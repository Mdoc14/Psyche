using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IShadow //Define los métodos que puede realizar el contexto
{
    //Gestión de Estados
    public IState GetState();
    public void SetState(IState state);
    public void RestoreShadow();

    //Gestión de Movimiento
    public GameObject GetEnemyGameObject();
    public GameObject GetPlayerAtSight();
    public float GetDistanceTo(Transform destination);
    public float GetAngleTo(Transform destination);
    public float GetBasicMovementSpeed();
    public float GetAttackingSpeed();
    public float GetRotationSpeed();
    public void MoveTo(Transform destination, float movementSpeed);
    public void RotateTo(Transform destination);

    //Gestión de Detección
    public GameObject CheckIfPlayerIsAtSight(GameObject other);

    //Gestión de Waypoints
    public Transform[] GetWaypoints();
    public Transform GetCurrentWaypoint();
    public void SetCurrentWaypoint(Transform waypoint);
}
