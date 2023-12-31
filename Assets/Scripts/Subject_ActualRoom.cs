using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject_ActualRoom : MonoBehaviour, ISubject<float>
{
    //Atributos de la clase

    //Lista con los observadores del sujeto
    public List<IObserver<float>> observadores = new List<IObserver<float>>();

    //Entero que indicara el numero de sala
    float room = 1;
    
    //Suscribe al observador recibido a este sujeto
    public void AddObserver(IObserver<float> observer)
    {
        observadores.Add(observer);
    }

    //Notifica a todos los observadores de la lista, le pasa el valor de room a UpdateObserver()
    public void NotifyObservers()
    {
        foreach (IObserver<float> obs in observadores) 
        {
            obs.UpdateObserver(room);
        }
    }

    //Desuscribe al observadores recibido a este sujeto
    public void RemoveObserver(IObserver<float> observer)
    {
        observadores.Remove(observer);
    }

    //Al colisionar, en funcion del tag del trigger, cambiara el valor de room y hara una llamada al metodo Notify()
    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Habitacion":
                room = 1;
                break;
            case "Pasillo":
                room = 2;
                break;
            case "Servicio":
                room = 3;
                break;
            case "Oficina":
                room = 4;
                break;
            case "PasilloP0":
                room = 5;
                break;
            case "Cocina":
                room = 6;
                break;
            case "Comedor":
                room = 7;
                break;
            case "Salon":
                room = 8;
                break;
            case "PlantaBaja":
                room = 9;
                break;
            case "Piso1":
                room = 10;
                break;
            case "Jardin":
                room = 11;
                break;
            case "SalonJardin":
                room = 12;
                break;
        }
        NotifyObservers();

    }
}
