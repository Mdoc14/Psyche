using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LocustParameters", order = 1)]
public class LocustParameters : ScriptableObject
{
    public float speed; //Cantidad de fuerza aplicada para el movimiento
    public float scale; //Escala del objeto
    public float Unaccuracy; //Desviación aleatoria del movimiento
    public float maxDistanceToTarget; //Distancia a partir de la cual los objetos se moverán a por el objetivo
    public bool lethal; //Parámetro que indica si los objetos pueden matar al jugador
    public Transform cameraPos; //Referencia compartida a la posición de la cámara

    public void setLethal(bool set) //Función compartida para cambiar "lethal"
    {
        lethal = set;
    }

    public void setCameraPos(Transform cam) //Función compartida para establecer la referencia a la cámara
    {
        cameraPos = cam;
    }
}
