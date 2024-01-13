using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneController : MonoBehaviour
{
    public Animator changeFadeAnimator;
    public GameObject finalObject; // Objeto con componente de la clase Activable con el que se interactua para cambiar de escena
    public bool interactuable; // Comprueba si el cambio de escena será por interacción o no

    public void Update()
    {
        //Comprobamos si el cambio de escena es por interacción del jugador o no
        if (interactuable)
        {
            //Comprobamos que se ha activado el objeto final de la escena, ya que se destruye al interactuar
            if (finalObject==null)
            {
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

        //En funcion de la escena actual, cambia a una u otra
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene("3_StreetScene");
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
        if (!interactuable)// En caso de no ser por interacción, cuando el player entre en el collider pasará a la siguiente escena 
        {
            nextScene();
        }
    }
}