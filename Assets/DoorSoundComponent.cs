using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundComponent : MonoBehaviour
{
    public AudioSource openDoor;
    public AudioSource closeDoor;
    public GameObject doorInteractuable;
    private bool isClosed;

    private void Awake()
    {
        openDoor = GetComponents<AudioSource>()[0];
        closeDoor = GetComponents<AudioSource>()[1];
        isClosed = true;
    }

    private void Update()
    {
        if (doorInteractuable.GetComponent<IInteractable>().isActivated())
        {
            if (isClosed)
            {
                openDoor?.Play();
                isClosed = false;
            }
            else 
            {
                closeDoor?.Play();
                isClosed = true;
            }
        }
    }
}


