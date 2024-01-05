using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayer : AShadowState
{
    bool playerIsDead = false;

    float timeSincePlayerDied = 0.0f;

    public AttackingPlayer(IShadow shadow) : base(shadow)
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
        if (shadow.GetDistanceTo(shadow.GetPlayerAtSight().transform) > 1f)
        {
            shadow.MoveTo(shadow.GetPlayerAtSight().transform, shadow.GetAttackingSpeed());
        }
        else
        {
            playerIsDead = true;
            shadow.GetPlayerAtSight().GetComponent<PlayerController>().Die();
        }
    }

    public override void Update()
    {
        if (playerIsDead)
        {
            timeSincePlayerDied += Time.deltaTime;
        }

        if(timeSincePlayerDied >= 3.5)
        {
            shadow.RestoreShadow();
        }
    }
}
