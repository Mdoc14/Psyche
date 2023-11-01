using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject_ActualRoom : MonoBehaviour, ISubject<float>
{
    //Atributos de la clase

    //Lista con los observadores del sujeto
    public List<IObserver<float>> observadores = new List<IObserver<float>>();

    //Entero que indicará el numero de sala
    float room = 1;
    
    //Suscribe al observador recibido a este sujeto
    public void AddObserver(IObserver<float> observer)
    {
        observadores.Add(observer);
    }

    //Notifica a todos los observadores de la lista
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

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Habitacion")) 
        {
            room = 1;
        }

        if (other.CompareTag("Pasillo"))
        {
            room = 2;
        }

        if (other.CompareTag("Servicio"))
        {
            room = 3;
        }

        if (other.CompareTag("Oficina"))
        {
            room = 4;
        }

        if (other.CompareTag("PasilloP0"))
        {
            room = 5;
        }

        if (other.CompareTag("Cocina"))
        {
            room = 6;
        }

        if (other.CompareTag("Comedor"))
        {
            room = 7;
        }

        if (other.CompareTag("Salon"))
        {
            room = 8;
        }

        if (other.CompareTag("PlantaBaja"))
        {
            room = 9;
        }

        NotifyObservers();

    }
}
