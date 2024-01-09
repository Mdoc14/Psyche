using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInAudioComponent : MonoBehaviour
{
    public AudioSource houseIn;
    public AudioSource houseOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !houseIn.isPlaying)
        {
            houseOut.Stop();
            houseIn.Play();
        }
    }
}
