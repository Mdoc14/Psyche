using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void Play() 
    {
        SceneManager.LoadScene("Intro");
    }

    public void Quit() 
    {
        Debug.Log("Saliendo del juego ...");
        Application.Quit();
    }
}
