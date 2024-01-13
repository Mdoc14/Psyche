using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour //Componente utilizado para la transicionar a la siguiente escena desde las "cinemáticas"
{
    AudioSource audioSource; //Referencia al audio de la cinemática

    //Al inicio obtenemos el componente de audio que se encuentra en el GameObject portador del AudioController
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Si el audio ya no se está reproduciendo
        if (!audioSource.isPlaying)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1) //Si estamos en la cinemática inicial
            {
                SceneManager.LoadScene("2_HouseScene"); //Pasamos al primer nivel
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6) //Si estamos en la cinemática final
            { 
                SceneManager.LoadScene("7_CreditsScene"); //Pasamos a la escena de créditos
            }
        }
    }
}
