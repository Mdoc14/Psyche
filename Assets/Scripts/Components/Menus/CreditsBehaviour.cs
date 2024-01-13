using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float limit;
    [SerializeField]
    private float timer;
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); //El panel de los cr�ditos se desplaza hacia arriba con una velocidad

        //Con un timer se controla cuando se acaban los cr�ditos, pasando a la escena del men�
        if (timer > limit) 
        {
            GoToMainMenu();
        }
        timer += Time.deltaTime;
    }
    public void GoToMainMenu() 
    {
        SceneManager.LoadScene("0_MainMenu");
    }
}
