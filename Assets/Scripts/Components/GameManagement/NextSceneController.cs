using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneController : MonoBehaviour
{
    public Animator changeFadeAnimator;
    public GameObject finalObject;
    public bool interactuable;

    public void Update()
    {
        //Comprobamos si el cambio de escena es por intracción del jugador o no
        if (interactuable)
        {
            //Comprobamos que se ha activado el objeto final de la escena
            if (finalObject==null)
            {
                Debug.Log("Activado");
                nextScene();
                interactuable = false;
            }
        }
    }
    public void nextScene()
    {
        if (changeFadeAnimator != null)
        {
            changeFadeAnimator.SetTrigger("BlackOut");
        }
        Invoke("LoadNextScene", 2f);//Se llama a LoadNextScene() a los 2 segundos
    }

    private void LoadNextScene()
    {
        changeFadeAnimator.ResetTrigger("BlackOut");
        Console.WriteLine("Nos vamos pa la calle1");
        Debug.Log("Nos vamos pa la calle. Debug1");

        //En funcion de la escena actual, cambia a una u otra
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene("3_StreetScene");
            Console.WriteLine("Nos vamos pa la calle2");
            Debug.Log("Nos vamos pa la calle. Debug2");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3) 
        {
            SceneManager.LoadScene("4_HospitalScene");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene("5_FinalScene");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene("6_End");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!interactuable) {
            nextScene();
        }
    }
}