using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShadowController : MonoBehaviour, IShadow //Contexto del Patr�n State
{
    //ATRIBUTOS CONFIGURABLES DEL ENEMIGO
    public int movementSpeed; //Velocidad de movimiento del enemigo cuando patrulla
    public int attackingSpeed; //Velocidad de movimiento del enemigo cuando se dirige al jugador
    public int rotationSpeed; //Velocidad de rotaci�n del enemigo
    public int viewingAngle; //Angulo de visi�n
    public Transform[] waypoints; //Array de waypoints que recorrer� el enemigo

    //Referencia al estado actual
    private IState _currentState;

    //Atributos para controlar al enemigo
    private Transform _visionArea;
    private Vector3 _initialPosition;
    private Transform _currentWaypoint; //Referencia a la transformada del waypoint actual
    private GameObject _playerAtSight = null; //Referencia al jugador si est� a la vista del enemigo

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
        _visionArea = transform.Find("VisionArea");
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
        float angle = Vector3.Angle(transform.forward, direction.normalized);

        return angle;
    }

    public int GetBasicMovementSpeed()
    {
        return movementSpeed;
    }

    public int GetAttackingSpeed()
    {
        return attackingSpeed;
    }

    public int GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public void MoveTo(Transform destination, float speed)
    {
        Vector3 direction = destination.position - transform.position;

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

            Vector3 rayTarget = player.transform.position;
            rayTarget.y = _visionArea.position.y;

            if (Physics.Linecast(_visionArea.position, rayTarget, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Player"))
                {
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
        if(other.CompareTag("Player"))
        {
            _playerAtSight = CheckIfPlayerIsAtSight(other.gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerAtSight = CheckIfPlayerIsAtSight(other.gameObject);
        }
    }
}
