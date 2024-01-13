using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState //Interfaz que define todos los métodos que un estado debe ejecutar
{
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
}
