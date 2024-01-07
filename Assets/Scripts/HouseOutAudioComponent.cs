using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOutAudioComponent : MonoBehaviour
{
    public AudioSource houseIn;
    public AudioSource houseOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !houseOut.isPlaying)
        {
            houseIn.Stop();
            houseOut.Play();
        }
    }
}
