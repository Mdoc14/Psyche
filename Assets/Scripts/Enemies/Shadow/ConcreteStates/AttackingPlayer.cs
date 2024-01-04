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
        shadow.GetEnemyGameObject().transform.Find("Character2").GetComponent<Collider>().enabled = false;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        if (shadow.GetDistanceTo(shadow.GetPlayerAtSight().transform) > 0.5f)
        {
            shadow.MoveTo(shadow.GetPlayerAtSight().transform, shadow.GetAttackingSpeed());
        }
        else
        {
            shadow.GetPlayerAtSight().GetComponent<PlayerController>().Die();
            playerIsDead = true;
        }
    }

    public override void Update()
    {
        if (playerIsDead)
        {
            timeSincePlayerDied += Time.deltaTime;
        }

        if(timeSincePlayerDied >= 5)
        {
            shadow.RestoreShadow();
        }
    }
}
