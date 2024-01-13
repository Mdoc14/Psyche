using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInAudioComponent : MonoBehaviour
{
    public AudioSource houseIn; //Sonido ambiente de casa en interior
    public AudioSource houseOut; //Sonido ambiente de casa en exterior

    private void OnTriggerEnter(Collider other)
    {
        // Si el player entra en el collider de dentro de la casa, empieza a sonar el sonido ambiente interno
        if (other.CompareTag("Player") && !houseIn.isPlaying)
        {
            houseOut.Stop();
            houseIn.Play();
        }
    }
}
