using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShadowController : MonoBehaviour, IShadow //Contexto del Patrón State
{
    //ATRIBUTOS CONFIGURABLES DEL ENEMIGO
    public float movementSpeed; //Velocidad de movimiento del enemigo cuando patrulla
    public float attackingSpeed; //Velocidad de movimiento del enemigo cuando se dirige al jugador
    public float rotationSpeed; //Velocidad de rotación del enemigo
    public float viewingAngle; //Angulo de visión
    public Transform[] waypoints; //Array de waypoints que recorrerá el enemigo

    //Referencia al estado actual
    private IState _currentState;

    //Atributos para controlar al enemigo
    private Vector3 _initialPosition;
    private Transform _currentWaypoint; //Referencia a la transformada del waypoint actual
    private GameObject _playerAtSight = null; //Referencia al jugador si está a la vista del enemigo

    public IState GetState()
    {
        return _currentState;
    }

    public void SetState(IState state)
    {
        if(_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = state;

        _currentState.Enter();
    }

    void Awake()
    {
        _initialPosition = transform.position;
        _currentWaypoint = waypoints[0];
        SetState(new LookingForAWaypoint(this));
    }

    void Update()
    {
        _currentState.Update();
    }


    void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    public void RestoreShadow()
    {
        transform.position = _initialPosition;
        _currentWaypoint = waypoints[0];
        _playerAtSight = null;
        SetState(new LookingForAWaypoint(this));
    }

    public GameObject GetEnemyGameObject()
    {
        return gameObject;
    }

    public GameObject GetPlayerAtSight()
    {
        return _playerAtSight;
    }

    public float GetDistanceTo(Transform destination)
    {
        Vector3 direction = destination.position - transform.position;
        direction.y = 0;

        return direction.magnitude;
    }

    public float GetAngleTo(Transform destination)
    {
        Vector3 direction = destination.position - transform.position;
        direction.y = 0;
        float angle = Vector3.SignedAngle(transform.forward, direction.normalized, transform.up);

        return angle;
    }

    public float GetBasicMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetAttackingSpeed()
    {
        return attackingSpeed;
    }

    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public void MoveTo(Transform destination, float speed)
    {
        Vector3 direction = (destination.position - transform.position).normalized;

        if (GetAngleTo(destination) != 0)
        {
            RotateTo(destination);
        }

        transform.position += speed * Time.fixedDeltaTime * direction;
    }

    public void RotateTo(Transform destination)
    {
        transform.Rotate(transform.up, rotationSpeed * Time.fixedDeltaTime * GetAngleTo(destination));
    }

    public GameObject CheckIfPlayerIsAtSight(GameObject player)
    {
        if (Math.Abs(GetAngleTo(player.transform)) < (viewingAngle / 2))
        {
            RaycastHit hit;

            Vector3 rayOrigin = transform.position;
            rayOrigin.y = transform.position.y + 1.4f;

            Vector3 rayTarget = player.transform.position;
            rayTarget.y = transform.position.y + 1.4f;

            int layerMask = ~(1 << LayerMask.NameToLayer("Only Raycast")); //El linecast ignorará esta capa

            if (Physics.Linecast(rayOrigin, rayTarget, out hit, layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    SetState(new AttackingPlayer(this));
                    return hit.collider.gameObject;
                }
            }
        }

        return null;
    }

    public Transform[] GetWaypoints()
    {
        return waypoints;
    }

    public Transform GetCurrentWaypoint()
    {
        return _currentWaypoint;
    }

    public void SetCurrentWaypoint(Transform waypoint)
    {
        _currentWaypoint = waypoint;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && _playerAtSight == null)
        {
            _playerAtSight = CheckIfPlayerIsAtSight(other.gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _playerAtSight == null)
        {
            _playerAtSight = CheckIfPlayerIsAtSight(other.gameObject);
        }
    }
}
