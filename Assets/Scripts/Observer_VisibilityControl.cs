using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer_VisiilityControl : MonoBehaviour, IObserver<float>
{
    //Atributos de la clase

    //GameObject que contiene el mapa
    public GameObject mapa;
    private List<GameObject> salaActual;

    //Listas con las paredes, suelos y objetos del mapa
    public List<GameObject> habitacion = new List<GameObject>();
    public List<GameObject> pasillo = new List<GameObject>();
    public List<GameObject> servicio = new List<GameObject>();
    public List<GameObject> oficina = new List<GameObject>();
    public List<GameObject> pasilloP0 = new List<GameObject>();
    public List<GameObject> cocina = new List<GameObject>();
    public List<GameObject> comedor = new List<GameObject>();
    public List<GameObject> salon = new List<GameObject>();

    private void Awake()
    {
        //Buscamos al jugador y suscribimos esta clase a su sujeto
        GameObject.FindWithTag("Player").GetComponent<Subject_ActualRoom>().AddObserver(this);

        quitRoom(pasillo);
        quitRoom(servicio);
        quitRoom(oficina);
        quitRoom(cocina);
        quitRoom(comedor);
        quitRoom(salon);
        quitRoom(pasilloP0);

        salaActual = habitacion;
        chargeRoom(salaActual);

    }

    //Recorre la lista y pone en false el atributo "enabled" del MeshRenderer. Ocultando los GameObject que no queremos que se vean 
    public void quitRoom(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    //Recorre la lista y pone en true el atributo "enabled" del MeshRenderer. Mostrando los GameObject que queremos que se vean
    public void chargeRoom(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    //Desactiva la sala actual y carga la siguiente sala
    public void updateEnviroment(float room, List<GameObject> currentRoom, List<GameObject> nextRoom)
    {
        quitRoom(currentRoom);
        chargeRoom(nextRoom);
        salaActual = nextRoom;
    }
    public void UpdateObserver(float room)
    {
        switch (room)
        {
            case 1: //Sala actual --> Habitacion
                updateEnviroment(1, salaActual, habitacion);
                break;

            case 2: //Sala actual --> Pasillo
                updateEnviroment(2, salaActual, pasillo);
                break;

            case 3: //Sala actual --> Servicio
                updateEnviroment(3, salaActual, servicio);
                break;

            case 4: //Sala actual --> Oficina
                updateEnviroment(4, salaActual, oficina);
                break;

            case 5: //Sala actual --> Pasillo planta baja
                updateEnviroment(5, salaActual, pasilloP0);
                break;

            case 6: //Sala actual --> Cocina
                updateEnviroment(6, salaActual, cocina);
                break;

            case 7: //Sala actual --> Comedor
                updateEnviroment(7, salaActual, comedor);
                break;

            case 8: //Sala actual --> Salon
                updateEnviroment(8, salaActual, salon);
                break;

            default:
                break;

        }
    }


}
