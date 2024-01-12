using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOnceSoundComponent : MonoBehaviour
{
    private AudioSource source;
    public GameObject ObjectWithAudioSource;
    public GameObject objectInteractuable;
    private bool active;
    private bool soundPlayed;

    private void Awake()
    {
        source = ObjectWithAudioSource.GetComponent<AudioSource>();
        soundPlayed = true;
    }

    private void Update()
    {
        active = objectInteractuable.activeSelf;
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
