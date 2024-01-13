using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayer : AShadowState
{
    bool playerIsDead;

    float timeSincePlayerDied = 0.0f;

    public AttackingPlayer(IShadow shadow) : base(shadow)
    {
    }

    public override void Enter()
    {
        playerIsDead = false;
    }

    public override void Exit()
    {
        playerIsDead = false;
    }

    public override void FixedUpdate()
    {
        Debug.Log("Atacando");
        if (shadow.GetDistanceTo(shadow.GetPlayerAtSight().transform) > 0.8f)
        {
            shadow.MoveTo(shadow.GetPlayerAtSight().transform, shadow.GetAttackingSpeed());
        }
        else
        {
            if (!playerIsDead)
            {
                playerIsDead = true;
                shadow.GetPlayerAtSight().GetComponent<PlayerController>().Die();
            }
        }
    }

    public override void Update()
    {
        if (playerIsDead)
        {
            timeSincePlayerDied += Time.deltaTime;
        }

        if(timeSincePlayerDied >= 3.25)
        {
            shadow.RestoreShadow();
        }
    }
}
