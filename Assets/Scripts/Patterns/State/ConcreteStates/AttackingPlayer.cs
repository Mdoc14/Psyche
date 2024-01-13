using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayer : AShadowState
{
    private bool playerIsDead; //Booleano que indica si el jugador ha sido o no matado

    private float timeSincePlayerDied = 0.0f; //Contador de tiempo que incrementa a partir de la muerte del jugador

    public AttackingPlayer(IShadow shadow) : base(shadow)
    {
    }

    public override void Enter()
    {
        playerIsDead = false; //Al empezar a atacar, el jugador debe estar vivo
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        //Si la distancia al jugador es mayor que 1 y sigue vivo nos moveremos hacia él
        if (shadow.GetDistanceTo(shadow.GetPlayerAtSight().transform) > 1f && !playerIsDead)
        {
            shadow.MoveTo(shadow.GetPlayerAtSight().transform, shadow.GetAttackingSpeed());
        }
        else //En caso contrario
        {
            if (!playerIsDead) //Si no ha sido matado
            {
                //Muere el jugador
                playerIsDead = true;
                shadow.GetPlayerAtSight().GetComponent<PlayerController>().Die();
            }
        }
    }

    public override void Update()
    {
        //Si el jugador está muerto incrementamos el contador
        if (playerIsDead)
        {
            timeSincePlayerDied += Time.deltaTime;
        }

        //Tras un periodo de 3.75 segundos, el enemigo vuelve a su estado inicial (un instante después de que reaparezca el jugador)
        if(timeSincePlayerDied >= 3.75)
        {
            shadow.RestoreShadow();
        }
    }
}
