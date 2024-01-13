using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOnceSoundComponent : MonoBehaviour
{
    private AudioSource source;
    public GameObject ObjectWithAudioSource;
    public GameObject objectInteractuable;
    private bool active; // Comprueba que el objeto interactuable esta activo o no
    private bool soundPlayed; // Comprueba que el sonido solo suene una vez

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
                soundPlayed  = false; // Si el sonido todavía no ha sonado, se pone soundPlayed a false para que no suene más
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
