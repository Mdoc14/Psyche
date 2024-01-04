using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LocustParameters", order = 1)]
public class LocustParameters : ScriptableObject
{
    public float speed; //Cantidad de fuerza aplicada para el movimiento
    public float scale; //Escala del objeto
    public float Unaccuracy; //Desviaci�n aleatoria del movimiento
    public float maxDistanceToTarget; //Distancia a partir de la cual los objetos se mover�n a por el objetivo
    public bool lethal; //Par�metro que indica si los objetos pueden matar al jugador
    public Transform cameraPos; //Referencia compartida a la posici�n de la c�mara

    public void setLethal(bool set) //Funci�n compartida para cambiar "lethal"
    {
        lethal = set;
    }

    public void setCameraPos(Transform cam) //Funci�n compartida para establecer la referencia a la c�mara
    {
        cameraPos = cam;
    }
}
