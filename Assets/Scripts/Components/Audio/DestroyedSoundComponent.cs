using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedSoundComponent : MonoBehaviour
{
    private AudioSource source;
    public GameObject playerSound;
    public GameObject objectActive;
    private bool active;
    private bool soundPlayed;

    private void Awake()
    {
        source = playerSound.GetComponent<AudioSource>();
        soundPlayed = true;
    }

    private void Update()
    {
        active = objectActive.activeSelf;
        if (!active) 
        {
            if (soundPlayed)
            {
                soundPlayed  = false;
                source.Play();
            }
        }
        else
        {
            soundPlayed = true;
        }
    }

    private void OnDestroy()
    {
        if (source != null)
        {
            source.Play();
        }
    } 
}
