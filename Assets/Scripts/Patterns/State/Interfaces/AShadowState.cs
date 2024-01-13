using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AShadowState : IState //Todos los estados concretos heredan esta clase que implementa la funcionalidad común
{
    protected IShadow shadow; //Referencia al contexto sobre la que realizar acciones desde los estados

    public AShadowState(IShadow shadow)
    {
        this.shadow = shadow;
    }

    //Dejamos la implementación de los métodos a los estados concretos
    public abstract void Enter();
    public abstract void Exit();
    public abstract void FixedUpdate();
    public abstract void Update();
}
