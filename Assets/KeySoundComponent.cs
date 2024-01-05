using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySoundComponent : MonoBehaviour
{
    private AudioSource keyPicked;
    public GameObject player;

    private void Awake()
    {
        keyPicked = player.GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        if (keyPicked != null)
        {
            keyPicked.Play();
        }
    } 
}
