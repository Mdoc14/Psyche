using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedSoundComponent : MonoBehaviour
{
    private AudioSource source;
    public GameObject playerSound;

    private void Awake()
    {
        source = playerSound.GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        if (source != null)
        {
            Debug.Log("Llegué");
            source.Play();
        }
    } 
}
