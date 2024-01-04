using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AShadowState : IState
{
    protected IShadow shadow;

    public AShadowState(IShadow shadow)
    {
        this.shadow = shadow;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void FixedUpdate();
    public abstract void Update();
}
