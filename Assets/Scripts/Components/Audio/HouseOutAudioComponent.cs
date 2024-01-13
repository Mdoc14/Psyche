using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOutAudioComponent : MonoBehaviour
{
    public AudioSource houseIn; //Sonido ambiente de casa en interior
    public AudioSource houseOut; // Sonido ambiente de casa en exterior

    private void OnTriggerEnter(Collider other)
    {
        // Si el player entra en el collider de fuera de la casa, empieza a sonar el sonido ambiente externo 
        if (other.CompareTag("Player") && !houseOut.isPlaying)
        {
            houseIn.Stop();
            houseOut.Play();
        }
    }
}
