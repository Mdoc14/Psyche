using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject panel;
    public void Play() 
    {
        SceneManager.LoadScene("Intro");
    }

    public void OpenOptions() 
    { 
        panel.SetActive(true);
    }
    public void CloseOptions() 
    {
        panel.SetActive(false);
    }
    public void Quit() 
    {
        Debug.Log("Saliendo del juego ...");
        Application.Quit();
    }
}
