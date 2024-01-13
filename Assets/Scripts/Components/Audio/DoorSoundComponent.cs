using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundComponent : MonoBehaviour
{
    public AudioSource openDoor;
    public AudioSource closeDoor;
    public GameObject doorInteractuable; // Objeto interactuable de la puerta
    private bool isClosed; // Alterna el sonido de la puerta 

    private void Awake()
    {
        openDoor = GetComponents<AudioSource>()[0];
        closeDoor = GetComponents<AudioSource>()[1];
        isClosed = true;
    }

    private void Update()
    {
        // Cuando se activa la puerta, el sonido se reproduce en funcion de isClosed
        if (doorInteractuable.GetComponent<Activable>().isActivated())
        {
            if (isClosed && openDoor != null)
            {
                openDoor.Play();
                isClosed = false;
            }
            else if(!isClosed && closeDoor != null) 
            {
                closeDoor.Play();
                isClosed = true;
            }
        }
    }
}


