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
    public AudioSource whisperAudio;//Sonido de ataque del enemigo

    //Referencia al estado actual
    private IState _currentState;

    //Atributos para controlar al enemigo
    private Vector3 _initialPosition; //Posición al inicio del nivel para restablecer el enemigo
    private Quaternion _initialRotation; //Rotación al inicio del nivel para restablecer el enemigo
    private Transform _currentWaypoint; //Referencia a la transformada del waypoint actual
    private GameObject _playerAtSight = null; //Referencia al jugador si está a la vista del enemigo

    public IState GetState()
    {
        return _currentState;
    }

    //Método de cambio de estados
    public void SetState(IState state)
    {
        if(_currentState != null)
        {
            _currentState.Exit(); //Se llama siempre que salimos de un estado
        }

        _currentState = state;

        _currentState.Enter(); //Se llama siempre que entramos a un nuevo estado
    }

    void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _currentWaypoint = waypoints[0];
        SetState(new LookingForAWaypoint(this)); //Al principio el enemigo se dirigirá al siguiente Waypoint
    }

    //Delegamos la ejecución del comportamiento a los estados
    void Update()
    {
        _currentState.Update();
    }


    void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    //Restablece al enemigo a su estado inicial
    public void RestoreShadow()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _currentWaypoint = waypoints[0];
        _playerAtSight = null;
        SetState(new LookingForAWaypoint(this)); //Volvemos a buscar el siguiente waypoint
    }

    public GameObject GetEnemyGameObject()
    {
        return gameObject;
    }

    public GameObject GetPlayerAtSight()
    {
        return _playerAtSight;
    }

    //Devuelve el valor de la distancia desde el enemigo a la transformada destino
    public float GetDistanceTo(Transform destination)
    {
        Vector3 direction = destination.position - transform.position;
        direction.y = 0;

        return direction.magnitude;
    }

    //Devuelve el ángulo en grados entre el vector forward del enemigo y el vector dirección a la transformada destino
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

    //Mueve al enemigo a la transformada destino con la velocidad especificada
    public void MoveTo(Transform destination, float speed)
    {
        Vector3 direction = (destination.position - transform.position).normalized;

        if (GetAngleTo(destination) != 0) //Si no está mirando hacia su destino también rota
        {
            RotateTo(destination);
        }

        transform.position += speed * Time.fixedDeltaTime * direction;
    }

    //Rota al enemigo para mirar en la dirección de la transformada destino
    public void RotateTo(Transform destination)
    {
        transform.Rotate(transform.up, rotationSpeed * Time.fixedDeltaTime * GetAngleTo(destination));
    }

    //Devuelve el GameObject del jugador si está a la vista del enemigo
    public GameObject CheckIfPlayerIsAtSight(GameObject player)
    {
        //Si este método ha sido llamado es que el player está cerca del enemigo. Dentro de su collider de visión

        //Si el ángulo entre el vector forward del enemigo y el vector dirección al jugador es menor que el ángulo de visión entre 2, 
        //el jugador se encuentra en el campo de visión del enemigo
        if (Math.Abs(GetAngleTo(player.transform)) < (viewingAngle / 2))
        {
            RaycastHit hit;

            Vector3 rayOrigin = transform.position;
            rayOrigin.y = transform.position.y + 1.4f;

            Vector3 rayTarget = player.transform.position;
            rayTarget.y = transform.position.y + 1.4f;

            int layerMask = ~(1 << LayerMask.NameToLayer("Only Raycast")); //El linecast ignorará esta capa

            //Se comprueba si hay otro objeto en la trayectoria de un rayo desde el enemigo al player. De esta forma, el jugador puede
            //esconderse detrás de paredes sin ser visto.
            if (Physics.Linecast(rayOrigin, rayTarget, out hit, layerMask))
            {
                if (hit.collider.CompareTag("Player")) //Si la colisión del rayo ha sido con el Player, no había objetos intermedios
                {
                    SetState(new AttackingPlayer(this)); //Pasamos al estado de ataque al jugador
                    whisperAudio.Play();
                    return hit.collider.gameObject; //Devolvemos una referencia del GameObject del Player para realizar cálculos de dirección
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

    //Ya sea al entrar el jugador en el Trigger o al permanecer en él.Si no se ha encontrado un jugador antes, se comprueba si está a la
    //vista. Nuestro enemigo mata al jugador siempre que lo ve, no puede huir de él, es por esta razón que no se comprueba si deja de estar
    //a la vista.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && _playerAtSight == null)
        {
            _playerAtSight = CheckIfPlayerIsAtSight(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _playerAtSight == null)
        {
            _playerAtSight = CheckIfPlayerIsAtSight(other.gameObject);
        }
    }
}
